import { Component, OnInit, ViewChild } from '@angular/core';
import { MeterService } from '../../services/meter.service';
import { MeterModel } from '../../models/meter.model';
import { DataSourceModel } from 'src/app/shared/models/data-source.model';
import { ExcelService } from 'src/app/shared/services/excel.service';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
import * as jsPDF from 'jspdf';
import 'jspdf-autotable';
import { ReportService } from 'src/app/modules/report/services/report.service';
import { HttpHelperService } from 'src/app/shared/services/http-heler.service';
import { QuickDialogComponent } from 'src/app/shared/components/quick-dialog/quick-dialog.component';
import { MatDialog } from '@angular/material';
import { QuickDialogEntitiesEnum } from 'src/app/shared/enums/quick-dialog-entities.enum';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';
import { PageEnum } from 'src/app/shared/enums/page.enum';
import { PagePrivilegeModel } from 'src/app/shared/models/page-privilege.model';
import { PrivilegeEnum } from 'src/app/shared/enums/privilege.enum';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { ChangeGroupDialogComponent } from 'src/app/modules/device-group/components/change-group-dialog/change-group-dialog.component';
import { DeviceGroupService } from 'src/app/modules/device-group/services/device-group.service';
import { ChangeVendorDialogComponent } from 'src/app/modules/device-vendor/components/change-vendor-dialog/change-vendor-dialog.component';
import { DeviceVendorService } from 'src/app/modules/device-vendor/services/device-vendor.service';
import { ChangeMeterModelDialogComponent } from 'src/app/modules/meter-model/components/change-meter-model-dialog/change-meter-model-dialog.component';
import { MeterModelService } from 'src/app/modules/meter-model/services/meter-model.service';
@Component({
	selector: 'app-meter-list',
	templateUrl: './meter-list.component.html',
	styleUrls: ['./meter-list.component.css']
})
export class MeterListComponent implements OnInit {
	allMeter: Array<MeterModel> = new Array<MeterModel>();
	public dataSource: DataSourceModel = new DataSourceModel();
	public hcn: string = null;
	public vendorId: number;
	public groupId: number;
	public privileges = [];
	constructor(private httpHelperService: HttpHelperService,
		private meterService: MeterService,
		private excelService: ExcelService,
		private reportService: ReportService,
		private dialog: MatDialog,
		private deviceGroupService: DeviceGroupService,
		private deviceVendorService: DeviceVendorService,
		private meterModelService: MeterModelService,
		private authSrvice: AuthService) {
		this.dataSource.CurrentPage = 1;
		pdfMake.vfs = pdfFonts.pdfMake.vfs;
	}

	ngOnInit() {
		let allPriveilges = this.authSrvice.getUserPrivilegesFromLocalStorage();
		this.privileges = allPriveilges.filter(x => x.page_id == PageEnum.Meter);
		this.getAllMeterDetails();
		this.dataSource.Keyword = null;
	}

	getAllMeterDetails() {
		this.meterService.getAllMetersAsync(this.dataSource, this.hcn, this.vendorId, this.groupId).subscribe((res) => {
			this.setNewMeters(res);
		});
	}

	public getMeterVendorDetailsById(id: number) {
		this.dialog.open(QuickDialogComponent, { data: { entityName: QuickDialogEntitiesEnum.DeviceVendor, id: id } })///Make Vendor Enum
	}
	//>>>END Meter Vendor
	private setNewMeters(meterList: any) {
		for (let meter of meterList) {
			this.allMeter.push(meter);
		}
	}

	onScroll() {
		this.dataSource.CurrentPage = this.dataSource.CurrentPage + 1;
		this.getAllMeterDetails();
	}
	public search() {
		this.allMeter = [];
		this.getAllMeterDetails();
	}

	public exportAsXLSX(): void {
		this.excelService.exportAsExcelFile(this.allMeter, 'Meters');
	}

	generatePdf() {
		let title = "Meters";
		let meters = document.getElementById("meters").innerHTML;
		let html = '<h1 class="center">Meters</h1>' + meters;
		this.reportService.createPDF(html).subscribe((res) => {
			res = `${this.httpHelperService.baseUrl}${res}`;
			window.open(res, '_blank');
		});
	}

	public onDevicVendorChange(deviceVendor: any) {
		this.vendorId = deviceVendor.vendorId;
	}

	public onDevicGroupChange(deviceGroup: any) {
		this.groupId = deviceGroup.groupId;
	}

	public onActionclick(pagePrivilegeModel: PagePrivilegeModel, meter: MeterModel) {
		switch (pagePrivilegeModel.privilege_id) {
			case PrivilegeEnum.Delete:
				this.showConfirmDialog(meter);
				break;
			case PrivilegeEnum.ChangeGroup:
				this.showGroupDialog(meter);
				break;
			case PrivilegeEnum.ChangeVendor:
				this.showVendorDialog(meter);
				break;
			case PrivilegeEnum.ChangeComModel:
				this.showMeterModelDialog(meter);
				break;

			default:
				break;
		}
	}

	showConfirmDialog(meter: MeterModel): void {
		const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
			width: '350px',
			data: "Do you confirm the deletion of this data?"
		});
		dialogRef.afterClosed().subscribe(result => {
			if (result) {
				this.delete(meter.meterId);
			}
		});
	}

	private delete(meterId: any) {
		this.meterService.delete(meterId).subscribe((res) => {
			this.allMeter = this.allMeter.filter(x => x.meterId != meterId);
		});
	}

	showGroupDialog(meter: MeterModel): void {
		const dialogRef = this.dialog.open(ChangeGroupDialogComponent, {
			width: '350px',
			data: { selectedGroupId: meter.groupId }
		});
		dialogRef.afterClosed().subscribe(result => {
			if (result) {
				meter.groupId = this.deviceGroupService.deviceGroupModel.groupId;
				this.update(meter);
			}
		});
	}

	showVendorDialog(meter: MeterModel): void {
		const dialogRef = this.dialog.open(ChangeVendorDialogComponent, {
			width: '350px',
			data: { selectedVendorId: meter.vendorId }
		});
		dialogRef.afterClosed().subscribe(result => {
			if (result) {
				meter.vendorId = this.deviceVendorService.deviceVendorModel.vendorId;
				this.update(meter);
			}
		});
	}

	showMeterModelDialog(meter: MeterModel): void {
		const dialogRef = this.dialog.open(ChangeMeterModelDialogComponent, {
			width: '350px',
			data: { selectedMeterModelId: meter.modelId }
		});
		dialogRef.afterClosed().subscribe(result => {
			if (result) {
				meter.modelId = this.meterModelService.meterModelModel.modelId;
				this.update(meter);
			}
		});
	}

	private update(meter: any) {
		this.meterService.update(meter).subscribe((res) => {
			// this.allMeter = this.allMeter.filter(x => x.meter_Id != meterId);
		});
	}
}