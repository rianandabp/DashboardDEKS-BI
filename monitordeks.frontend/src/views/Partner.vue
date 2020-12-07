<template>
    <b-container fluid class="container">
        <b-row no-gutters>
            <b-col> <SideMenu/> </b-col>
            <b-col cols="9" class="partner" >
                <b-row class="button"> 
                    <b-button size="sm" variant="primary" @click="$bvModal.show('dp')"> Create Partner </b-button>
                    <b-modal id="dp" @ok="addPartner(selectedProgram)" @hidden="resetModal()" >
                        <b-form-group label="Select Program" >
                            <b-form-select class="bform" v-model="selectedProgram">
                                <b-form-select-option v-for="p in program" :value="p" :key="p.id" >{{p.name}}</b-form-select-option>
                            </b-form-select>
                        </b-form-group>

                        <b-form-group label="Partner Name">
                            <b-form-input
                                size="sm"
                                placeholder="Write the partner name.."
                                v-model="partner.partnerName"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group label="Partner Location">
                            <b-form-input
                                size="sm"
                                placeholder="Write the partner location.."
                                v-model="partner.location"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group label="Partner Caretaker" >
                            <b-form-input
                                size="sm"
                                placeholder="Write the partner caretaker name.."
                                v-model="partner.careTakerName"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group label="Partner Phone Number" >
                            <b-form-input
                                size="sm"
                                placeholder="Write the phone number.."
                                v-model="partner.careTakerPhoneNumber"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group label="Link to upload">
                            <b-form-input
                                size="sm"
                                placeholder="Enter link for partner to upload the document.."
                                v-model="partner.uploadLink"
                            ></b-form-input>
                        </b-form-group>
                        
                        <b-form-group label="Caretaker Username">
                            <b-form-input
                                size="sm"
                                placeholder="Write the username for login.."
                                v-model="partner.usernameId"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group label="Password for login">
                            <b-form-input
                                size="sm"
                                placeholder="Write the password for login.."
                                v-model="partner.password"
                                type="password"
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group label="Re-type password" label-for="dropdown-form-password">
                        <b-form-input v-model="pass" :state="passState()" aria-describedby="input-live-pass" placeholder="Re-type password.." type="password" size="sm"> </b-form-input>
                            <b-form-invalid-feedback id="input-live-pass">
                                Password is not same!
                            </b-form-invalid-feedback>
                        </b-form-group>
                    </b-modal>

                    <b-button id="sr" size="sm" variant="primary" @click="$router.push('/managepartner')"> Manage Partner </b-button>

                    <b-button id="sr" size="sm" variant="primary" @click="$bvModal.show('dp2')"> See Report </b-button>
                    <b-modal id="dp2">
                        <b-form-group label="Select Program">
                            <b-form-select class="bform" v-model="selectedProgram1" @change="loadPartner()">
                                <b-form-select-option v-for="p in program" :value="p" :key="p.id" >{{p.name}}</b-form-select-option>
                            </b-form-select>
                        </b-form-group>
                        
                        <b-form-group label="Select Partner">
                            <b-form-select class="bform" v-model="selectedPartner" @change="loadReport(selectedPartner.usernameId)">
                                <b-form-select-option v-for="p in partners" :value="p" :key="p.usernameId" >{{p.partnerName}}</b-form-select-option>
                            </b-form-select>
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
                            
                        </tr>
                        <tr v-for="(item,index) in reports" :key="item.id">

                            <td class="idx"> {{index + 1}} </td>
                            <td class="date"> {{item.createdOn | moment("MMMM Do YYYY")}} </td>
                            <td class="progress1"> <b-progress id="bp" :value="item.progress" :max=100 show-progress animated></b-progress> </td>
                            <td class="keterangan" id="mn"> <b-icon icon="journals" @click="$bvModal.show('A-'+item.id)"></b-icon> </td>
                            <td class="kendala"> <b-icon icon="clipboard-x" @click="$bvModal.show('B-'+item.id)"></b-icon> </td>
                            <td class="rencana"> <b-icon icon="bootstrap-reboot" @click="$bvModal.show('C-'+item.id)"></b-icon> </td>
                            <td class="lampiran"> <b-link :href="item.document">{{item.document}}</b-link> </td>

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
                <b-col> <Notification/></b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SideMenu from "@/components/SideMenu.vue";
import Notification from "@/components/Notification.vue";
import {IProgram} from "@/types/Program.d.ts";
import {IPartner} from "@/types/Partner.d.ts";
import {ProgramService} from "@/services/product-service";
import { PartnerService } from "@/services/partner-service";
import { PartnerReportService} from "@/services/partner-report-service";
import { IPartnerReport } from '@/types/PartnerReport';

const partnerService = new PartnerService();
const programService = new ProgramService();
const reportService = new PartnerReportService();

@Component({
  name: "App",
  components: {SideMenu, Notification}
})


export default class Partner extends Vue {

    program: IProgram[] = [];
    partners: IPartner[] = [];

    selectedProgram: IProgram = {}
    selectedProgram1: IProgram = { id: 0 }
    selectedPartner: IPartner = { caretakerPhoneNumber: ''}

    partner: IPartner = {
        partnerName: '',
        caretakerPhoneNumber: ''
    };

    reports: IPartnerReport = {}

    pass: string = '';

    passState() {
        if(this.pass === '') return null;   
        return (this.pass === this.partner.password) ? true : false
    }

    resetModal(){
        this.partner.partnerName = '';
        this.partner.location = '';
        this.partner.caretakerName = '';
        this.partner.caretakerPhoneNumber = '';
        this.partner.caretakerPhoneNumber = '';
        this.partner.usernameId = '';
        this.partner.password = '';
        this.pass = '';
    }

    async addPartner() {
        if(!this.passState()) return;
        console.log(this.partner.caretakerPhoneNumber);
        this.partner.programId = this.selectedProgram.id;
        await partnerService.addPartner(this.partner);
        await this.initialize();
    }
    
    async loadReport(username:string){
        console.log(username);
        this.reports = await reportService.getPartnerReport(username);
    }

    async loadPartner(){
        this.partners = await partnerService.getPartner(this.selectedProgram1.id!);
    }

    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === "null") return this.$router.push('/login');
        this.program = await programService.getProgram();
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

.partner{
    height: 900px;
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