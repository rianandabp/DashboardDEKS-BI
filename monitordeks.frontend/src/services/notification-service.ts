import axios from "axios";
import {INotification} from "@/types/Notification.d.ts";

export class NotificationService{
    API_URL = process.env.VUE_APP_API_URL;

    public async getNotification(): Promise<any> {
        const result: any = await axios.get(`${this.API_URL}/notification`);
        return result.data;
    }

    public async addNotification(notification:INotification): Promise<any> {
        const result: any = await axios.post(`${this.API_URL}/notification`,notification);
        return result.data;
    }
}