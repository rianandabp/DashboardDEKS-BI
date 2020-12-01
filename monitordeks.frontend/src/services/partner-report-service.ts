import axios from "axios";
import {IPartnerReport} from "@/types/PartnerReport.d.ts";
import { IServiceResponse } from "@/types/ServiceResponse";

export class PartnerReportService{
    API_URL = process.env.VUE_APP_API_URL;


    public async getPartnerReport(username:string): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/partner/report/${username}`);
        return result.data;
    };

    public async addReport(newReport: IPartnerReport): Promise<IServiceResponse<IPartnerReport>> {
        console.log(newReport);
        const result: any = await axios.post(`${this.API_URL}/partner/report`, newReport);
        return result.data;
    };

    public async deleteReport(id: number): Promise<boolean> {
        const result: any = await axios.delete(`${this.API_URL}/partner/report/${id}`);
        return result.data;
    };
}