import axios from "axios";
import {ITask} from "@/types/Task.d.ts";
import { IServiceResponse } from "@/types/ServiceResponse";

export class TaskService{
    API_URL = process.env.VUE_APP_API_URL;

    public async getTask(): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/task`);
        return result.data;
    }

    public async getTotal(): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/task/count`);
        return result.data;
    }

    public async getTaskByProgramId(id:number): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/task/${id}`);
        return result.data;
    }

    public async getTaskByOutlineId(id:number): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/task/outline/${id}`);
        return result.data;
    }

    public async deleteTask(Id: number): Promise<boolean> {
        const result: any = await axios.delete(`${this.API_URL}/task/${Id}`);
        return result.data;
    };

    public async updateTaskStatus(Id: number): Promise<boolean> {
        const result: any = await axios.patch(`${this.API_URL}/task/${Id}`);
        return result.data;
    };

    public async addTask(newTask: ITask): Promise<IServiceResponse<ITask>> {
        const result: any = await axios.post(`${this.API_URL}/task`, newTask);
        return result.data;
    };

    public async updateTask(newTask: ITask): Promise<IServiceResponse<ITask>> {
        const result: any = await axios.patch(`${this.API_URL}/task`, newTask);
        return result.data;
    };
}