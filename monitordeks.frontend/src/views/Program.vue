<template>
    <b-container fluid class="container">
        <b-row no-gutters>
            <b-col> <SideMenu/> </b-col>
            <b-col cols="9" class="program" >
                <b-col>
                    <b-row>
                        <b-button id="bb" variant="primary" size="sm" @click="$bvModal.show('dp2')">Add Program</b-button>
                        <b-modal id="dp2" @hidden="resetModal" @ok="addProgram(temp)">
                            <b-form-group label="Program Name">
                                <b-form-input
                                    required
                                    value="Ininih"
                                    size="sm"
                                    placeholder="Write the program name.."
                                    v-model="temp.name"
                                    :state="nameState()"
                                    aria-describedby="input-live-name"
                                ></b-form-input>
                                <b-form-invalid-feedback id="input-live-username">
                                    Max input length is 18 characters.
                                </b-form-invalid-feedback>
                            </b-form-group>

                            <b-form-group label="Description" @submit.stop.prevent>
                            <b-form-input
                                required
                                size="sm"
                                placeholder="Write description.."
                                v-model="temp.description"
                            ></b-form-input>
                            </b-form-group>

                            <b-form-group label="Start Date" @submit.stop.prevent>
                                <b-form-datepicker required size="sm" v-model="temp.startDate" class="mb-2"></b-form-datepicker>
                            </b-form-group>

                            <b-form-group label="End Date" @submit.stop.prevent>
                                <b-form-datepicker required size="sm" v-model="temp.endDate" class="mb-2"></b-form-datepicker>
                            </b-form-group>
                        </b-modal> 
                    </b-row>
                    <b-row> 
                        <div v-for="item in program" :key="item.id">
                    <b-card
                        :title="item.name"
                        img-height="90rem"
                        style="max-width: 20rem;"
                        class="mb-2"
                        
                        
                    >
                        <b-card-text class="text">{{item.description}} </b-card-text>
                        
                        <div class="button">
                            <div id="visit"> <b-button variant="success" @click="programSession(item.id,item.name),$router.push('/program/details')"> Visit </b-button></div>
                            <div id="edit">
                                <b-button id="be" variant="primary" @click="$bvModal.show('dp3-' + item.id)"> Edit </b-button>
                                <b-modal :id="'dp3-' + item.id" @show="loadModal(item.id)" @hidden="resetModal" @ok="updateProgram(item.id)">
                                    <b-form-group label="Program Name" label-for="dropdown-form-email" @submit.stop.prevent>
                                        <b-form-input
                                            required
                                            size="sm"
                                            placeholder="Write the program name.."
                                            v-model="temp.name"
                                            :state="nameState()"
                                            aria-describedby="input-live-name"
                                        ></b-form-input>
                                        <b-form-invalid-feedback id="input-live-username">
                                            Max input length is 18 characters.
                                        </b-form-invalid-feedback>
                                    </b-form-group>

                                    <b-form-group label="Description" @submit.stop.prevent>
                                    <b-form-input
                                        
                                        size="sm"
                                        placeholder="Write description.."
                                        v-model="temp.description"
                                    ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Start Date" @submit.stop.prevent>
                                        <b-form-datepicker required size="sm" v-model="temp.startDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>

                                    <b-form-group label="End Date" @submit.stop.prevent>
                                        <b-form-datepicker required size="sm" v-model="temp.endDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                </b-modal>
                            </div>
                            <div id="delete"> <b-button variant="danger" @click="$bvModal.show('confirmationDelete-' + item.id)"> Delete </b-button></div>
                            <b-modal :id="'confirmationDelete-' + item.id" @ok="deleteProgram(item.id)">
                                {{item.name}} program will be deleted
                            </b-modal>
                        </div>
                    </b-card>
                </div>
                    </b-row>
                </b-col>
                
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
import {IUser} from "@/types/User.d.ts";
import {ProgramService} from "@/services/product-service";
import { UserService } from "@/services/user-service";

const userService = new UserService();
const programService = new ProgramService();

@Component({
  name: "App",
  components: {SideMenu, Notification}
})


export default class Program extends Vue {

    program: IProgram[] = [];

    user: IUser[] = [];
    temp: IProgram = {
        name: ''
    };

    
    nameState() {
        return this.temp.name!.length > 18 ? false : null
    }

    programSession(id:number,name:string){
        sessionStorage.setItem("program", id.toString());
        sessionStorage.setItem("programName", name);
    }

    resetModal(){
        this.temp.name = '';
        this.temp.description = '';
        this.temp.startDate = undefined;
        this.temp.endDate = undefined;
    }

    loadModal(id:number){
        const p = this.program.find(c => c.id==id); 
        console.log(p?.name);
        this.temp.name = p?.name;
        this.temp.description = p?.description;
        this.temp.startDate = p?.startDate;
        this.temp.endDate = p?.endDate;
    }

    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === "null") return this.$router.push('/login');
        this.program = await programService.getProgram();
        this.user = await userService.getUser();
    }

    async created(){
        await this.initialize();
    }

    async deleteProgram(id:number){
        await programService.deleteProgram(id);
        await this.initialize();
    }

    async addProgram(newProgram: IProgram) {
        if(this.nameState()==false) return;
        await programService.addProgram(newProgram);
        await this.initialize();
    }

    async updateProgram(id: number){
        const p = this.program.find(c => c.id == id);
        p!.name = this.temp.name;
        p!.description = this.temp.description;
        p!.startDate = this.temp.startDate;
        p!.endDate = this.temp.endDate;

        await programService.updateProgram(p!);
        await this.initialize();
    }

    

}

</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";
.program{
    display: flex;
    flex-direction:row;
    max-height: 950px;
    overflow-y: scroll;

}


.dp{
    width: 200px;
}
.card{
    margin-top:20px ;
    margin-left:20px ;
    height: 200px;
    width: 350px;
}

.dp{
    width: 400px;
}
#bb{
    margin-left: 20px;
    margin-top: 20px;
}

#be{
    margin-top: 8px;
    margin-left: 3px;
    margin-right: 3px;
}

.text{
    height: 50px;
    text-overflow: ellipsis;
    overflow: hidden;
}


.mb-2, .my-2 {
    margin-bottom: 0rem!important;
}

.button{
    display: flex;
    flex-direction: row;
}

#delete{
    padding:8px ;
}

#visit{
    padding:8px ;
}
</style>