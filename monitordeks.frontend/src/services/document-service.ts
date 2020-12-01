import axios from "axios";
import {IDocument} from "@/types/Document.d.ts";
import { IServiceResponse } from "@/types/ServiceResponse";

export class DocumentService{
    API_URL = process.env.VUE_APP_API_URL;

    public async getDocumentByProgram(id:number): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/document/${id}`);
        return result.data;
    }

    public async getDocumentByTask(id:number): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/document/task/${id}`);
        return result.data;
    }

    public async addDocument(newDocument: IDocument): Promise<IServiceResponse<IDocument>> {
        const result: any = await axios.post(`${this.API_URL}/document`, newDocument);
        return result.data;
    };
}