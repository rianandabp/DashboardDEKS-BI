<template>
    <b-container fluid class="container">
        <b-row class="box" no-gutters>
            <b-col> <SideMenu/> </b-col>
            <b-col cols="9" > 
                <b-col class="main"> 
                    <b-row class="statbar" no-gutters> 
                        <b-col>  
                            <b-col class="detail" no-gutters>
                                <b-row> <h6> Progress </h6> </b-row>
                                <b-row> <b-progress :value="this.program.progressPercentage" :max=100 show-progress animated class="bar"></b-progress> </b-row>
                                
                            </b-col>
                        </b-col>
                        <b-col> <h3>{{programName}}</h3> </b-col>
                        <b-col class=taskstat>
                            <h6>Task Status</h6>  
                            <b-row no-gutters class="mid">
                                <b-col> <b-badge id="bs" variant="success"> {{this.program.taskComplete}} task(s) out of {{this.program.totalTask}} tasks is completed</b-badge></b-col>
                            </b-row>
                        </b-col>
                    </b-row>
                    <b-col class="content-container" no-gutters>
                        <b-row class="title">
                            <b-col> <h4> Outline </h4> </b-col> 
                            <b-col>
                                <b-button class="dp" variant="primary" size="sm" @click="$bvModal.show('dp1')">Add Outline</b-button>
                                <b-modal id="dp1" @hidden="resetOutline" @ok="addOutline()">
                                    <b-form-group label="Outline Name"  >
                                        <b-form-input
                                            size="sm"
                                            placeholder="Write the outline name.."
                                            v-model="temp.name"
                                        ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Deadline"  >
                                        <b-form-datepicker size="sm" v-model="temp.deadline" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                </b-modal>
                            </b-col>
                        </b-row>
                        <b-row class="content" v-for="(outline,index) in outline" :key="outline.id">
                            <b-col id="left"> <div id="ini"> <h6 id="name"> {{index + 1}}. <b-link @click="$bvModal.show('A-' + outline.id)">{{outline.name}}</b-link> </h6> </div> 
                                <b-progress :value="outline.progressPercentage" :max=100 show-progress animated id="bara"></b-progress>
                            </b-col>
                                <b-modal :id="'A-' + outline.id" @ok="editOutline(outline.id)" :title="'Edit Outline'">
                                    <b-form-group label="Outline Name"  >
                                        <b-form-input
                                            
                                            size="sm"
                                            placeholder="Write the outline name.."
                                            v-model="temp.name"
                                        ></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Progress Note">
                                        <b-form-textarea
                                        id="textarea"
                                        v-model="temp.progressNote"
                                        placeholder="Enter something..."
                                        rows="3"
                                        max-rows="6"
                                        ></b-form-textarea>
                                    </b-form-group>
                                    <b-form-group label="Problem Note">
                                        <b-form-textarea
                                            id="textarea2"
                                            v-model="temp.problemNote"
                                            placeholder="Enter something..."
                                            rows="3"
                                            max-rows="6"
                                        ></b-form-textarea>
                                    </b-form-group>
                                    <b-form-group label="Deadline"  >
                                        <b-form-datepicker size="sm" v-model="temp.deadline" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                    <b-button id="dlt" size="sm" variant="danger" @click="deleteOutline(outline.id)"> Delete</b-button>
                                </b-modal>
                            <div id="right">
                                <b-button class="task-button" variant="outline-primary" size="sm" @click="$bvModal.show('Z-' + outline.id)"> Add Task </b-button>
                                <b-modal :id="'Z-' + outline.id" @hidden="resetTask" @ok="addTask(outline.id,selectedUser.name)"  :title="'Add Task'">
                                    <b-form-group label="Task Name"  >
                                        <b-form-input
                                            type="text"
                                            size="sm"
                                            placeholder="Write the task name.."
                                            v-model="tempTask.name"
                                        ></b-form-input>
                                    </b-form-group>
                                    <b-form-group label="Deadline"  >
                                        <b-form-datepicker size="sm" v-model="tempTask.deadline" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                    <b-form-group label="Assign to">
                                        <b-form-select v-model="selectedUser" class="bform">
                                            <b-form-select-option v-for="u in user" :value="u" :key="u.name">{{u.name}}</b-form-select-option>
                                        </b-form-select>
                                    </b-form-group>
                                </b-modal>
                            </div> 
                                <table class="table">
                                    <tr>
                                        <th id="deadline"> Deadline </th>
                                        <th class="status"> Status </th>   
                                        <th id="name"> Name </th>
                                        <th id="assignedTo"> Assigned To</th>
                                        <th class="details"> Details </th>
                                        <th class="edit"> Edit </th>
                                        <th class="delete"> Delete </th>
                                    </tr>
                                    <tr v-for="item in task" :key="item.id">
        
                                        <td v-if="item.outlineId == outline.id"> {{item.deadline | moment("MMMM Do YYYY")}} </td>
                                        <td class="status" v-if="item.outlineId == outline.id">
                                            <b-form-checkbox :v-model="item.status" :v-bind:id="item.id" v-if="item.status" disabled checked="true"> </b-form-checkbox>

                                            <b-form-checkbox :v-model="item.status" :v-bind:id="item.id" v-if="item.status == false " :disabled="getAccess()" @change="item.status = true,
                                            updateStatus(item.id,outline.id),updateUserStatus(item.assignedTo,item.deadline)"> </b-form-checkbox>
                                            
                                        </td>
                                        <td v-if="item.outlineId == outline.id"><b-link id="nam" @click="stateButton(item.id)"> {{item.name}} </b-link> </td>
                                        <td v-if="item.outlineId == outline.id"> {{item.assignedTo}} </td>
                                        <td class="details" v-if="item.outlineId == outline.id"> <b-icon icon="journals" @click="$bvModal.show('A' + outline.id + item.id)"></b-icon> </td>
                                        <td class="edit" v-if="item.outlineId == outline.id"> <b-icon icon="clipboard" @click="$bvModal.show('B' + outline.id + item.id)"></b-icon> </td>
                                        <td id="delete"  v-if="item.outlineId == outline.id"> <b-icon icon="x-circle" @click="$bvModal.show('confirmationDelete-' + outline.id + item.id)"></b-icon> </td>
                                        <b-modal :id="'confirmationDelete-' + outline.id + item.id" @ok="deleteTask(item.id)">
                                            {{item.name}} will be deleted
                                        </b-modal>

                                        <b-modal :id="'A'+outline.id+item.id" :title="'Progress'">
                                            <h6 class="one"> Progress Notes </h6>
                                            <div class="textarea">
                                                {{item.progressNote}}
                                            </div>
                                            <h6 class="one"> Problems </h6>
                                            <div class="textarea">
                                                {{item.problemNote}}
                                            </div>
                                        </b-modal>

                                        <b-modal :id="'B'+outline.id+item.id" @show="loadTask(item.id)" @ok="editTask(item.id,selectedUser.name)" :title="'Edit Task'">
                                            <b-form-group label="Task Name" >
                                                <b-form-input
                                                    
                                                    size="sm"
                                                    placeholder="Write the task name.."
                                                    v-model="tempTask.name"
                                                ></b-form-input>
                                            </b-form-group>
                                            <b-form-group label="Deadline" >
                                                <b-form-datepicker size="sm" v-model="tempTask.deadline" class="mb-2"></b-form-datepicker>
                                            </b-form-group>
                                            <b-form-group label="Progress Note">
                                                <b-form-textarea
                                                id="textarea"
                                                v-model="tempTask.progressNote"
                                                placeholder="Enter something..."
                                                rows="3"
                                                max-rows="6"
                                                ></b-form-textarea>
                                            </b-form-group>
                                            <b-form-group label="Problem Note">
                                                <b-form-textarea
                                                    id="textarea2"
                                                    v-model="tempTask.problemNote"
                                                    placeholder="Enter something..."
                                                    rows="3"
                                                    max-rows="6"
                                                ></b-form-textarea>
                                            </b-form-group>
                                            <b-form-group label="Assign to">
                                                <b-form-select v-model="selectedUser" class="bform">
                                                    <b-form-select-option v-for="u in user" :value="u" :key="u.name">{{u.name}}</b-form-select-option>
                                                </b-form-select>
                                            </b-form-group>
                                        </b-modal>
                                    </tr>
                                </table>
                        </b-row>
                    </b-col>
                </b-col>
            </b-col>
            <b-col class="document"> 
                <h6 id="dc"> Document</h6>
                <b-button-group class="button">
                    <b-button size="sm" variant="outline-dark" @click="getAllDocument()">All</b-button>
                    <b-button size="sm" variant="outline-dark" :disabled.sync="this.state" @click="getDocumentByTask()">Selected</b-button>
                    <b-button size="sm" variant="outline-dark" :disabled.sync="this.state" @click="$bvModal.show('A')">Upload</b-button>
                </b-button-group>
                <b-modal :id="'A'" :title="'Progress'" @ok="addDocument()">
                    <b-form-group label="Document Name">
                        <b-form-input
                            
                            size="sm"
                            placeholder="Write document name.."
                            v-model="tempDocument.name"
                        ></b-form-input>
                    </b-form-group>
                    <b-form-group label="Document Link"  >
                        <b-form-input
                            
                            size="sm"
                            placeholder="Write document link.."
                            v-model="tempDocument.link"
                        ></b-form-input>
                    </b-form-group>
                    <b-form-group label="Description"  >
                        <b-form-textarea
                            id="textarea3"
                            v-model="tempDocument.description"
                            placeholder="Enter something..."
                            rows="3"
                            max-rows="6"
                            ></b-form-textarea>
                    </b-form-group>
                </b-modal>
                <b-row class="dc-list" no-gutters v-for="item in document" :key="item.id">
                    <b-col class="outs"> 
                        <b-row no-gutters id="bold"> {{item.name}}</b-row>
                        <b-row no-gutters> <b-link :href="'http://' + item.link"> {{item.link}}</b-link></b-row>
                        <b-row no-gutters> {{item.description}}</b-row>
                        <b-row no-gutters> {{item.uploadedOn | moment("MMMM Do YYYY")}}</b-row>
                    </b-col>
                </b-row>
            </b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SideMenu from "@/components/SideMenu.vue";
import Notification from "@/components/Notification.vue";
import {ITask} from "@/types/Task.d.ts";
import {IUser} from "@/types/User.d.ts";
import {IOutline} from "@/types/Outline.d.ts";
import {IDocument} from "@/types/Document.d.ts";
import { IProgram } from '@/types/Program';
import { INotification } from '@/types/Notification';
import {ProgramService} from "@/services/product-service";
import { TaskService } from "@/services/task-service";
import { OutlineService } from "@/services/outline-service";
import { DocumentService } from "@/services/document-service";
import { NotificationService } from "@/services/notification-service";
import { UserService } from "@/services/user-service";
import moment from 'moment'
import VueMoment from 'vue-moment';

Vue.prototype.moment = moment
Vue.use(VueMoment);

const taskService = new TaskService();
const programService = new ProgramService();
const outlineService = new OutlineService();
const documentService = new DocumentService();
const userService = new UserService();
const notificationService = new NotificationService();

@Component({
  name: "ProgramDetails",
  components: {SideMenu, Notification}
})


export default class ProgramDetails extends Vue {
    
    program: IProgram = {}
    taskId = 0;
    outline: IOutline[] = [];
    task: ITask[] = [];
    user: IUser[] = [];
    document: IDocument[] = [];
    selectedUser: IUser = {};
    temp: IOutline = {
        ProgramId: 0,
        progressNote: "Test",
        problemNote: "Test"
    };
    tempTask: ITask = {
        name: ''
    };
    notification: INotification = {};

    tempDocument: IDocument = {};   
    state = true;

    getAccess(){
        if(sessionStorage.getItem("access") === "administrator") return false;
        else return true;
    }
    
    get programSession(){
        return sessionStorage.getItem("program");
    }

    get programName(){
        return sessionStorage.getItem("programName");
    }

    resetTask(){
        this.tempTask.name = '';
        this.tempTask.deadline = undefined;
        this.tempTask.assignedTo = undefined;
    }

    loadTask(id:number){
        const p = this.task.find(c=> c.id==id);
        this.tempTask.name = p?.name;
        this.tempTask.deadline = p?.deadline;
        this.tempTask.progressNote = p?.progressNote;
        this.tempTask.problemNote = p?.progressNote;
    }
    resetOutline(){
        this.temp.name = '';
        this.temp.deadline = undefined;
    }

    stateButton(id:number){
        this.taskId = id;
        this.state = false;
    }

    async getDocumentByTask(){
        this.document = await documentService.getDocumentByTask(this.taskId);
    }

    async getAllDocument(){
        this.document = await documentService.getDocumentByProgram(this.program.id!); 
    }

    async addNotification(description:string){
        this.notification.description = description;
        await notificationService.addNotification(this.notification);
    }

    async addDocument(){
        const p = this.task.find(c => c.id == this.taskId);
        this.tempDocument.programId = this.program.id!;
        this.tempDocument.taskId = this.taskId;
        this.tempDocument.username = sessionStorage.getItem("username")!;
        await documentService.addDocument(this.tempDocument);
        this.addNotification(sessionStorage.getItem("username") + " just upload " + this.tempDocument.name +
        " in program " + this.program.name + " for " + p?.name + " task")
        await this.initialize();
    }

    async addOutline() {
        console.log(this.program.id);
        this.temp.ProgramId = this.program.id!;
        await outlineService.addOutline(this.temp);
        await this.initialize();
    }

    async updateUserStatus(name:string,deadline:Date){
        const p = this.user.find(c => c.name === name);
        p!.usernameId += '|';
        if(moment(deadline).isBefore(Date.now())) p!.usernameId! += 2;
        else p!.usernameId! += 1;

        console.log(p!.usernameId!);

        await userService.updateStatus(p!.usernameId!);
    }

    async updateStatus(id:number,outlineId:number) {
        await taskService.updateTaskStatus(id);
        await this.initialize();
    }

    async editOutline(outlineId:number) {
        this.temp.id = outlineId;
        await outlineService.editOutline(this.temp);
        await this.initialize();
    }

    async deleteOutline(outlineId:number) {
        await outlineService.deleteOutline(outlineId);
        await this.initialize();
    }

    async deleteTask(taskId:number) {
        await taskService.deleteTask(taskId);
        await this.initialize();
    }

    async addTask(outlineId: number, selectedUser: string) {
        this.tempTask.assignedTo = selectedUser;
        this.tempTask.outlineId = outlineId;
        this.tempTask.programId = this.program.id!;
        this.tempTask.status = false;
        await taskService.addTask(this.tempTask);
        await this.initialize();
    }

    async editTask(id: number, selectedUser: string) {
        this.tempTask.assignedTo = selectedUser;
        this.tempTask.id = id;
        await taskService.updateTask(this.tempTask);
        await this.initialize();
    }
    
    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === "null") return this.$router.push('/login');
        this.program = await programService.getProgramById(parseInt(this.programSession!));
        this.user = await userService.getUser();
        this.document = await documentService.getDocumentByProgram(this.program.id!);
        this.outline = await outlineService.getOutlineByProgramId(this.program.id!);
        this.task = await taskService.getTaskByProgramId(this.program.id!);
        console.info(this.outline.length);
    }

    async created(){
        await this.initialize();
    }
    
}
</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";

.container{
    height: 100vh;
}
.document{
    text-align: center;
    width: 100%;
    background-color:$solar-lightgrey;
    height: 100vh;
    overflow-y:scroll ;
}

.dc-list{
    font-size: 7.5pt;
    margin: 5px;
    text-align: left;
}
h6{
    text-align: center;
}

#bold{
    font-weight: bold;
}

#dlt{
    margin-left: 5px;
}
.outs{
    margin: 4px;
    padding: 6px;
    background-color: white;
    border-radius: 10px;
    border: 1px solid lightgrey;
}
#dc{
    padding-top:10px ;
}
h3{
    margin-top: 15px;
}
.button{
    padding: 8px;
    width: 95%;
    font-size: 9pt;
}

.statbar{
    background-color: $solar-lightgrey;
    text-align: center;
    
    border: 1px solid lightgrey;
}

.detail{
    text-align: center;
    border-right: 1px solid lightgrey;
    padding: 0;
    padding-left:30px ;
    padding-top: 5px ;
}

.deadline{
    width: 150px;

}

#deadline{
    width: 160px;
}

#name{
    width: 400px;
}

.status{
    text-align: center;
}

.details{
    text-align: center;
}

#bs{
    padding: 8px;
    margin-bottom: 8px;
}

.edit{
    text-align: center;
}

.delete{
    text-align: center;
}

#delete{
    text-align: center;
    color: red;
}
.taskstat{
    border-left: 1px solid lightgrey;
    padding: 0;
    padding-top:5px ;
}

.dp{
    width: 100px;
}

#itu{
    font-size: 7.5pt;
    margin-left: 10px;
    margin-bottom: 10px;
}
#ini{
    display: flex;
    flex-direction: row;
}
p{
    font-size: 10pt;
    margin-bottom:4px ;
}

td{
    max-width: 280px;
    overflow-x: hidden ;
    height: fit-content;
}
#nam{
    color: black;
}

.main{
    padding: 0;
}
.content{
    margin-top: 30px;
    margin: 0 auto;
    width: 80%;
}

.content-container{
    height: 92vh;
    padding: 0;
    overflow-y: scroll;
    width: 100%;
}
h4{
    text-align: right;
}

.outline{
    font-size: 8pt;
}

.title{
    margin-top: 30px;
}

#name{
    text-align: left;
}

#left{
    text-align: left;
    padding-left: 0;
}

#right{
    display: flex;
    flex-direction: row;
    text-align: right;
    padding: 0;
}

#task-name{
    width: 120px;
    margin-top: 24px;
    margin-right: 15px;
}

label{
    font-size: 8pt;
    margin: 0;
}

.task-button{
    height: 30px;
    margin-bottom: 10px;
    margin-left: 15px;
}
.progress{
    width: 90%;
    border: 1px solid lightgrey;
}

.textarea{
    width: 90%;
    height: 250px;
    border: 1px solid lightgray;
    border-radius: 10px;
    margin: 0 auto;
    margin-bottom: 15px;
    overflow-y:scroll ;
    padding: 10px;
}

.one{
    margin-bottom: 15px;
    margin-left: 25px;
}

#assignedTo{
    width: 150px;
}
#bara{
    width: 200px;
    margin-bottom: 10px;
}
</style>