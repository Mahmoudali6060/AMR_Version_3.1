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
				this.showConfirmDialog(meter.meter_Id);
				break;

			default:
				break;
		}
	}

	showConfirmDialog(id): void {
		const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
			width: '350px',
			data: "Do you confirm the deletion of this data?"
		});
		dialogRef.afterClosed().subscribe(result => {
			if (result) {
				this.delete(id);
			}
		});
	}

	private delete(meterId: any) {
		this.meterService.delete(meterId).subscribe((res) => {
			this.allMeter = this.allMeter.filter(x => x.meter_Id != meterId);
		});
	}
}