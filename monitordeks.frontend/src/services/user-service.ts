import axios from "axios";
import {IUser} from "@/types/User.d.ts";
import { IServiceResponse } from "@/types/ServiceResponse";

export class UserService{
    API_URL = process.env.VUE_APP_API_URL;

    public async getUser(): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/user`);
        return result.data;
    };

    public async getUserByUsername(username:string): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/user/${username}`);
        return result.data;
    };

    public async updateUser(newUser: IUser){
        const result: any = await axios.patch(`${this.API_URL}/user`, newUser);
        return result.data;
    };

    public async updateStatus(token: string){
        const result: any = await axios.patch(`${this.API_URL}/user/${token}`);
        return result.data;
    };

    public async deleteUser(username: string): Promise<boolean> {
        const result: any = await axios.delete(`${this.API_URL}/user/${username}`);
        return result.data;
    };

    public async addUser(newUser: IUser): Promise<IServiceResponse<IUser>> {
        const result: any = await axios.post(`${this.API_URL}/user`, newUser);
        return result.data;
    };

    public async auth(token: string): Promise<any>{
        const result: any =  await axios.get(`${this.API_URL}/user/auth/${token}`);
        return result.data;
    }
}