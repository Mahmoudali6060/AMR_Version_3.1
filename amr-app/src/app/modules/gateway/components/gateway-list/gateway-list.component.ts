import { Component, OnInit, ViewChild } from '@angular/core';
import { GatewayService } from '../../services/gateway.service';
import { GatewayModel } from '../../models/gateway.model';
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
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { PrivilegeEnum } from 'src/app/shared/enums/privilege.enum';
import { PagePrivilegeModel } from 'src/app/shared/models/page-privilege.model';
@Component({
	selector: 'app-gateway-list',
	templateUrl: './gateway-list.component.html',
	styleUrls: ['./gateway-list.component.css']
})
export class GatewayListComponent implements OnInit {
	allGateway: Array<GatewayModel> = new Array<GatewayModel>();
	public dataSource: DataSourceModel = new DataSourceModel();
	public gatewayCode: string = null;
	public vendorId: number;
	public groupId: number;
	public privileges = [];
	constructor(private httpHelperService: HttpHelperService,
		private gatewayService: GatewayService,
		private excelService: ExcelService,
		private reportService: ReportService,
		private dialog: MatDialog,
		private authService: AuthService) {
		this.dataSource.CurrentPage = 1;
		pdfMake.vfs = pdfFonts.pdfMake.vfs;
	}

	ngOnInit() {
		let allPriveilges = this.authService.getUserPrivilegesFromLocalStorage();
		this.privileges = allPriveilges.filter(x => x.page_id == PageEnum.Gateway);
		this.getAllGatewayDetails();
		this.dataSource.Keyword = null;
	}

	getAllGatewayDetails() {
		this.gatewayService.getAllGatewaysAsync(this.dataSource, this.gatewayCode, this.vendorId, this.groupId).subscribe((res) => {
			this.setNewGateways(res);
		});
	}

	public getGatewayVendorDetailsById(id: number) {
		this.dialog.open(QuickDialogComponent, { data: { entityName: QuickDialogEntitiesEnum.DeviceVendor, id: id } })///Make Vendor Enum
	}
	//>>>END Gateway Vendor
	private setNewGateways(gatewayList: any) {
		for (let gateway of gatewayList) {
			this.allGateway.push(gateway);
		}
	}

	onScroll() {
		this.dataSource.CurrentPage = this.dataSource.CurrentPage + 1;
		this.getAllGatewayDetails();
	}
	public search() {
		this.allGateway = [];
		this.getAllGatewayDetails();
	}

	public exportAsXLSX(): void {
		this.excelService.exportAsExcelFile(this.allGateway, 'Gateways');
	}

	public onDevicVendorChange(deviceVendor: any) {
		this.vendorId = deviceVendor.vendorId;
	}

	public onDevicGroupChange(deviceGroup: any) {
		this.groupId = deviceGroup.groupId;

	}

	generatePdf() {
		let title = "Gateways";
		let gateways = document.getElementById("gateways").innerHTML;
		let html = '<h1 class="center">Gateways</h1>' + gateways;
		this.reportService.createPDF(html).subscribe((res) => {
			res = `${this.httpHelperService.baseUrl}${res}`;
			window.open(res, '_blank');
		});
	}

	public onActionclick(pagePrivilegeModel: PagePrivilegeModel, gateway: GatewayModel) {
		switch (pagePrivilegeModel.privilege_id) {
			case PrivilegeEnum.Delete:
				this.showConfirmDialog(gateway.gateway_Id);
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

	private delete(gatewayId: any) {
		this.gatewayService.delete(gatewayId).subscribe((res) => {
			this.allGateway = this.allGateway.filter(x => x.gateway_Id != gatewayId);
		});
	}
}