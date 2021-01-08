<template>
    <b-row class="container" no-gutters>
        <b-col> <SideMenu/> </b-col>
        <b-col cols="9">
            <div class="box">
                <b-row> <b-col id="us"> 
                    <h4>Partner List</h4> </b-col> <b-col id="au"> </b-col>
                </b-row>
                <div class="table"> <table id="tb" >
                        <tr>
                            <th> No </th>
                            <th> Username </th>
                            <th> Name </th>   
                            <th> Location </th>
                            <th> Caretaker </th>
                            <th> Phone number </th>
                            <th> Edit </th>
                            <th> Delete </th>
                        </tr>
                        <tr v-for="(item,index) in partner" :key="item.usernameId">

                            <td> {{index + 1}} </td>
                            <td> {{item.usernameId}} </td>
                            <td> {{item.partnerName}} </td>
                            <td> {{item.location}} </td>
                            <td id="cn"> {{item.caretakerName}} </td>
                            <td> {{item.caretakerPhoneNumber}} </td>
                            <td> <b-icon icon="clipboard" @click="loadPartner(item.usernameId),$bvModal.show(item.usernameId)"></b-icon> </td>
                            <td> <b-icon id="dlt" icon="x-circle" @click="$bvModal.show('confirmationDelete-' + item.usernameId)"></b-icon> </td>
                            <b-modal :id="'confirmationDelete-' + item.usernameId" @ok="deleteUser(item.usernameId)">
                                {{item.partnerName}} partner will be deleted
                            </b-modal>
                            <b-modal :id="item.usernameId" :title="'Edit Partner'" @ok="updatePartner(item.usernameId)">
                                <b-form-group label="Name" label-for="text" @submit.stop.prevent>
                                    <b-form-input
                                        size="sm"
                                        placeholder="Name"
                                        v-model="partnerName"
                                    ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Location" label-for="text" @submit.stop.prevent>
                                    <b-form-input
                                        size="sm"
                                        placeholder="Location.."
                                        v-model="location"
                                    ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Caretaker" label-for="text" @submit.stop.prevent>
                                    <b-form-input
                                        size="sm"
                                        placeholder="Caretaker.."
                                        v-model="caretakerName"
                                    ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Phone number" label-for="text" @submit.stop.prevent>
                                    <b-form-input
                                        size="sm"
                                        placeholder="Phone number.."
                                        v-model="caretakerPhoneNumber"
                                    ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Link to upload">
                                        <b-form-input
                                            size="sm"
                                            placeholder="Enter link for partner to upload the document.."
                                            v-model="uploadLink"
                                        ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Password" label-for="dropdown-form-password">
                                    <b-form-input
                                        type="password"
                                        size="sm"
                                        placeholder="Password.."
                                        v-model="password"
                                    ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Re-type password" label-for="dropdown-form-password">
                                    <b-form-input v-model="pass" :state="passState()" aria-describedby="input-live-pass" placeholder="Re-type password.." type="password" size="sm"> </b-form-input>
                                        <b-form-invalid-feedback id="input-live-pass">
                                            Password is not same!
                                        </b-form-invalid-feedback>
                                    </b-form-group>

                                    
                            </b-modal>
                        </tr>
                    </table>
                </div>
            </div>

        </b-col>
        <b-col class="manage"> 
            
        </b-col>
    </b-row>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SideMenu from "@/components/SideMenu.vue";
import Notification from "@/components/Notification.vue";
import { IPartner } from "@/types/Partner.d.ts";
import { PartnerService } from "@/services/partner-service";
import Vuex from 'vuex'

Vue.use(Vuex)
const partnerService = new PartnerService();
@Component({
  name: "App",
  components: {SideMenu, Notification}
})
export default class App extends Vue {

    partner: IPartner[] = [];

    usernameId:string = ''
    password:string = ''
    partnerName:string = ''
    location:string = ''
    caretakerName:string = ''
    caretakerPhoneNumber:string = ''
    uploadLink:string = ''

    pass: string = '';

    async loadPartner(id:string){
        const p = await this.partner.find(c => c.usernameId==id);
        this.partnerName = p?.partnerName!;
        this.location = p?.location!;
        this.caretakerName = p?.caretakerName!;
        this.caretakerPhoneNumber = p!.caretakerPhoneNumber!;
        this.uploadLink = p?.uploadLink!;
        this.usernameId = p?.usernameId!;
    }

    
    passState() {
        if(this.pass === '') return null;
        if(this.pass === this.password) return true;
        return false; 
    }

    async updatePartner(Id:string) {
        if(!this.passState()) return;
        const p = this.partner.find(c => c.usernameId == Id);

        p!.password = this.password;
        p!.partnerName = this.partnerName;
        p!.location = this.location;
        p!.caretakerName = this.caretakerName;
        p!.caretakerPhoneNumber = this.caretakerPhoneNumber;
        p!.uploadLink = this.uploadLink;

        console.log(p);
        await partnerService.updatePartner(p!);
        await this.initialize();
    }

    async deleteUser(username:string){
        await partnerService.deletePartner(username);
        await this.initialize();
    }


    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === null) return this.$router.push('/login');
        this.partner = await partnerService.getAll();
    }

    async created(){
        await this.initialize();
    }
    

}
</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";
.container{
    width: 100%;
}

#dlt{
    color: red;
}
.container, .container-fluid, .container-lg, .container-md, .container-sm, .container-xl {
    width: 100%;
    padding-right: 0px;
    padding-left: 0px;
    margin-right: auto;
    margin-left: auto;
}

.dropdown-confirm-password{
    margin-top:10px ;
}

.header{
    display: flex;
    flex-direction: row;
}

.cpForm{
    width: 200px;
}
.box{
    text-align: center;
    background-color: $solar-lightgrey;
    margin-right: 10%;
    margin-left: 10%;
    margin-top: 5%;
    padding-top: 10px;
    border-radius:20px ;
    box-shadow: 5px 10px 18px #888888;
}


#cn{
    max-width: 50px;
    text-overflow: ellipsis;
    overflow: hidden;
}

.manage{
    background-color: $solar-lightgrey;

}


.table{
    height: 500px;
    overflow-y: scroll;
}

#au{
    text-align: left;
    margin-bottom: 15px;
}

#us{
    text-align: center;
    margin-left: 33%;
}

#tb{
    width: 100%;
}

.form{
    text-align: center;
}

</style>