import axios from "axios";
import {IPartner} from "@/types/Partner.d.ts";
import { IServiceResponse } from "@/types/ServiceResponse";

export class PartnerService{
    API_URL = process.env.VUE_APP_API_URL;

    public async getPartner(id:number): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/partner/${id}`);
        return result.data;
    };

    public async getAll(): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/partner`);
        return result.data;
    };

    public async getPartnerById(id:string): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/partner/by/${id}`);
        return result.data;
    };

    public async updatePartner(newPartner: IPartner){
        const result: any = await axios.patch(`${this.API_URL}/partner`, newPartner);
        return result.data;
    };

    public async deletePartner(username: string): Promise<boolean> {
        const result: any = await axios.delete(`${this.API_URL}/partner/${username}`);
        return result.data;
    };

    public async addPartner(newPartner: IPartner): Promise<IServiceResponse<IPartner>> {
        const result: any = await axios.post(`${this.API_URL}/partner`, newPartner);
        return result.data;
    };

    public async auth(token: string): Promise<any>{
        const result: any =  await axios.get(`${this.API_URL}/partner/auth/${token}`);
        return result.data;
    }
}