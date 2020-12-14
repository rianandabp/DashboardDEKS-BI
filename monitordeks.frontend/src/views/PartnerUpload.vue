<template>
    <b-container fluid class="container">
        <b-row no-gutters>
            <b-col> <PartnerMenu/> </b-col>
            <b-col cols="9" class="partner" >
                <b-row class="button"> 
                    <b-button size="sm" variant="primary" @click="$bvModal.show('dp')"> Create new Report </b-button>
                    <b-modal id="dp" @ok="addPartnerReport">
                        <b-form-group label="Progress(%)" >
                            <b-form-input
                                size="sm"
                                placeholder="Write (0-100)"
                                type="number"
                                :max="100"
                                v-model.number="partnerReport.progress"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group label="Keterangan" >
                            <b-form-textarea
                                placeholder="Enter something..."
                                rows="3"
                                max-rows="6"
                                v-model="partnerReport.description"
                            ></b-form-textarea>
                        </b-form-group>

                        <b-form-group label="Kendala" >
                            <b-form-textarea
                                placeholder="Enter something..."
                                rows="3"
                                max-rows="6"
                                v-model="partnerReport.problem"
                            ></b-form-textarea>
                        </b-form-group>

                        <b-form-group label="Tindak Lanjut" >
                            <b-form-textarea
                                placeholder="Enter something..."
                                rows="3"
                                max-rows="6"
                                v-model="partnerReport.solution"
                            ></b-form-textarea>
                        </b-form-group>

                        <b-form-group label="Silahkan upload lampiran atau foto di" >
                            <b-link :href="partner.uploadLink"> {{this.partner.uploadLink}} </b-link>
                        </b-form-group>

                        <b-form-group label="Link dokumen yang telah diupload" >
                            <b-form-textarea
                                placeholder="Silahkan salin link dokumen disini..."
                                rows="3"
                                max-rows="6"
                                v-model="partnerReport.document"
                            ></b-form-textarea>
                        </b-form-group>


                    </b-modal>
                </b-row>
                <b-row no-gutters id="tb">  
                    <table class="table">
                        <tr>
                            <th class="idx"> No </th>
                            <th class="date"> Date </th>
                            <th class="progress1"> Progress </th>   
                            <th class="keterangan"> Keterangan </th>
                            <th class="kendala"> Kendala</th>
                            <th class="rencana"> Tindak Lanjut </th>
                            <th class="lampiran"> Lampiran </th>
                            <th> Delete </th>
                            
                        </tr>
                        <tr v-for="(item,index) in reports" :key="item.id">

                            <td class="idx"> {{index + 1}} </td>
                            <td class="date"> {{item.createdOn | moment("MMMM Do YYYY")}} </td>
                            <td class="progress1"> <b-progress id="bp" :value="item.progress" :max=100 show-progress animated></b-progress> </td>
                            <td class="keterangan" id="mn"> <b-icon icon="journals" @click="$bvModal.show('A-'+item.id)"></b-icon> </td>
                            <td class="kendala"> <b-icon icon="clipboard-x" @click="$bvModal.show('B-'+item.id)"></b-icon> </td>
                            <td class="rencana"> <b-icon icon="bootstrap-reboot" @click="$bvModal.show('C-'+item.id)"></b-icon> </td>
                            <td class="lampiran"> <b-link :href="item.document">{{item.document}}</b-link> </td>
                            <td> <b-icon id="dlt" icon="x-circle" @click="$bvModal.show('confirmationDelete-' + item.id)"></b-icon> </td>
                            <b-modal :id="'confirmationDelete-' + item.id" @ok="deleteReport(item.id)">
                                {{item.createdOn | moment("MMMM Do YYYY")}} report will be deleted
                            </b-modal>

                            <b-modal :id="'A-'+item.id" :title="'Progress'">
                                <h6 class="one"> Keterangan </h6>
                                <div class="textarea">
                                    {{item.description}}
                                </div>
                            </b-modal>
                            <b-modal :id="'B-'+item.id" :title="'Progress'">
                                <h6 class="one"> Kendala </h6>
                                <div class="textarea">
                                    {{item.problem}}
                                </div>
                            </b-modal>
                            <b-modal :id="'C-'+item.id" :title="'Progress'">
                                <h6 class="one"> Tindak Lanjut </h6>
                                <div class="textarea">
                                    {{item.solution}}
                                </div>
                            </b-modal>
                        </tr>
                        
                    </table>
                </b-row>
                </b-col>
                <b-col id="fg"> </b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import PartnerMenu from "@/components/PartnerMenu.vue";
import Notification from "@/components/Notification.vue";
import {IProgram} from "@/types/Program.d.ts";
import {IPartnerReport} from "@/types/PartnerReport.d.ts";
import {PartnerReportService} from "@/services/partner-report-service";
import {PartnerService} from "@/services/partner-service";
import { IPartner } from "@/types/Partner";
import { NotificationService } from "@/services/notification-service";
import { ProgramService } from "@/services/product-service";
import { INotification } from '@/types/Notification';

const partnerReportService = new PartnerReportService();
const partnerService = new PartnerService();
const notificationService = new NotificationService();
const programService = new ProgramService();

@Component({
  name: "App",
  components: {PartnerMenu, Notification}
})


export default class PartnerUpload extends Vue {


    partnerReport: IPartnerReport = {
        createdOn: undefined,
        progress: 0
    };

    notification: INotification = {};
    reports: IPartnerReport[] = [];
    partner: IPartner = { caretakerPhoneNumber:''};
    program: IProgram = {};

    async addPartnerReport() {
        this.program = await programService.getProgramById(this.partner.programId!);
        if(this.partnerReport.progress! > 100) this.partnerReport.progress = 100;
        this.partnerReport.caretakerUsernameId = sessionStorage.getItem("username")!;
        await partnerReportService.addReport(this.partnerReport);
        this.addNotification(this.partner.partnerName + " just upload a report" +
        " for program " + this.program.name)
        await this.initialize();
    }

    async addNotification(description:string){
        this.notification.description = description;
        await notificationService.addNotification(this.notification);
    }

    async deleteReport(id:number){
        await partnerReportService.deleteReport(id);
        await this.initialize();
    }

    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === null) return this.$router.push('/login');
        this.reports = await partnerReportService.getPartnerReport(sessionStorage.getItem("username")!);
        this.partner = await partnerService.getPartnerById(sessionStorage.getItem("username")!);
    }

    async created(){
        await this.initialize();
    }
    

}
</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";

.button{
    margin: 0;
    margin-left: 20px;
    margin-top: 20px;
}


#dlt{
    color: red;
}

#fg{
    background-color: #f1f1f1;
}
.partner{
    height: 100vh;
    overflow-y: scroll;
}
#sr{
    margin-left: 20px;
}

#tb{
    width: 90%;
    margin: 0 auto;
    margin-top: 30px;
}

.lampiran{
    max-width: 100px;
    text-overflow: ellipsis;
    overflow: hidden;
}

.keterangan{
    text-align: center;
}

.textarea{
    max-width: 90%;
    height: 250px;
    border: 1px solid lightgray;
    border-radius: 10px;
    margin: 0 auto;
    margin-bottom: 15px;
    overflow-y:scroll ;
    padding: 10px;
}

.kendala{
    text-align: center;
}

.rencana{
    text-align: center;
}

</style>