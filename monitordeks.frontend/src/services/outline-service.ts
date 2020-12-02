import axios from "axios";
import {IOutline} from "@/types/Outline.d.ts";
import { IServiceResponse } from "@/types/ServiceResponse";

export class OutlineService{
    API_URL = process.env.VUE_APP_API_URL;

    public async getOutline(): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/outline`);
        return result.data;
    }

    public async getOutlineByProgramId(id:number): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/outline/${id}`);
        return result.data;
    }

    public async addOutline(newOutline: IOutline): Promise<IServiceResponse<IOutline>> {
        const result: any = await axios.post(`${this.API_URL}/outline`, newOutline);
        return result.data;
    };
    
    public async editOutline(newOutline: IOutline): Promise<IServiceResponse<IOutline>> {
        const result: any = await axios.patch(`${this.API_URL}/outline`, newOutline);
        return result.data;
    };

    public async deleteOutline(Id: number): Promise<boolean> {
        await axios.delete(`${this.API_URL}/task/by/outline/${Id}`);
        const result: any = await axios.delete(`${this.API_URL}/outline/${Id}`);
        return result.data;
    };
    
}