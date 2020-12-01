import axios from "axios";
import {IProgram} from "@/types/Program.d.ts";
import {IOutline} from "@/types/Outline.d.ts";
import { IServiceResponse } from "@/types/ServiceResponse";

export class ProgramService{
    API_URL = process.env.VUE_APP_API_URL;

    public async getProgram(): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/program`);
        return result.data;
    }

    public async getProgramById(id:number): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/program/${id}`);
        return result.data;
    }

    public async updateProgram(newProgram:IProgram){
        const result: any = await axios.patch(`${this.API_URL}/program/`, newProgram);
        return result.data;
    };

    public async deleteProgram(Id: number): Promise<boolean> {
        const result: any = await axios.delete(`${this.API_URL}/program/${Id}`);
        return result.data;
    };

    public async addProgram(newProgram: IProgram): Promise<IServiceResponse<IProgram>> {
        const result: any = await axios.post(`${this.API_URL}/program`, newProgram);
        return result.data;
    };
}