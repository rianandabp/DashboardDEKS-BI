<template>
    <b-row no-gutters>
        <b-col> <SideMenu/> </b-col>
        <b-col cols="9"> 
            <b-col class="col-box" no-gutters>
                <b-row class="row-box" no-gutters>
                    <b-col id="totalProgram">
                        <h5>Total Program</h5>
                        <p> {{this.program.length}} Program(s)</p> 
                    </b-col>
                    <b-col id="totalTask">
                        <h5>Total Task</h5>
                        <p>{{total}} Task(s)</p>
                    </b-col>
                    <b-col id="hvv">
                        <b-row no-gutters class="hvvr"> <h5>Havengers of the Month <b-button id="vb" v-b-toggle.sidebar-right size="sm" variant="primary">See list</b-button> </h5> </b-row>
                        <b-sidebar id="sidebar-right" title="Sidebar" right shadow>
                        <div class="px-3 py-2">
                            <b-row no-gutters id="hv" v-for="(item,index) in user" :key="item.id">
                                <b-col v-if="index > 0" id="item-name"> {{item.name}}</b-col>
                                <b-col v-if="index == 0" id="item-nam"> {{item.name}}</b-col>
                                <b-col cols="8"> 
                                    <h6> {{item.taskDone}} task(s) done</h6>
                                    <h6> {{item.taskLate}} task(s) late</h6>
                                </b-col>
                            </b-row>
                        </div>
                        </b-sidebar>
                    </b-col>
                </b-row>
                <b-row class="dashboard-box" no-gutters>
                    <b-col id="program">
                        <p> Program Progress </p>
                        
                        <div class="card" v-for="item in program" :key="item.id">
                            <b-card
                                :title= "item.name"
                                img-height="90rem"
                                style="max-width: 20rem;"
                                class="mb-2"
                                id="my-card"
                                
                            >
                                <b-row>
                                    <b-col> Start Date: <br> {{item.startDate | moment("MMMM Do YYYY")}}</b-col>
                                    <b-col> End Date: <br> {{item.endDate | moment("MMMM Do YYYY")}} </b-col>
                                </b-row>
                                
                                <b-col class="detail">
                                    <b-row> Progress </b-row>
                                    <b-row> <b-progress class="b" :value="item.progressPercentage" :max="100" show-progress animated></b-progress> </b-row>
                                    <b-row> {{item.totalTask-item.taskComplete}} task(s) left </b-row>
                                </b-col>
                                
                                <b-badge class="badge" variant="success" v-if="item.programStatus == 1"> ON SCHEDULE </b-badge>
                                <b-badge class="badge" variant="warning" v-if="item.programStatus == 2"> YOU HAVE DEADLINE TODAY </b-badge>
                                <b-badge class="badge" variant="danger" v-if="item.programStatus == 3"> OUT OF SCHEDULE </b-badge>
                            </b-card>
                        </div>
                    </b-col>
                    <b-col id="havengers"> <p> Outline Progress </p>
                        <b-form-select class="bform" v-model="selectedProgram" @change="getOutlineByProgramId(selectedProgram.id)">
                            <b-form-select-option v-for="p in program" :value="p" :key="p.id" >{{p.name}}</b-form-select-option>
                        </b-form-select>
                        <b-row class="task-item" no-gutters v-for="outline in outline" :key="outline.id">
                            <b-col>
                                <b-row>
                                    <b-col class="task-left" id="iff"> {{outline.name}} </b-col>
                                    <b-col class="task-detail"> {{outline.deadline | moment("MMMM Do YYYY")}} </b-col>
                                </b-row>
                                <b-row>
                                    <b-col> <b-progress id="bp" :value="outline.progressPercentage" :max=100 show-progress animated></b-progress> </b-col>
                                    <b-col>
                                        <b-button id="tt" size="sm" @click="$bvModal.show('A' + outline.id)" v-if="outline.progressPercentage < 100 && !moment(outline.deadline).isBefore(Date.now())" variant="outline-dark">Details</b-button>
                                        <b-button id="tt" size="sm" @click="$bvModal.show('A' + outline.id)" v-if="outline.progressPercentage >= 100" variant="outline-success">Details</b-button>
                                        <b-button id="tt" size="sm" @click="$bvModal.show('A' + outline.id)" v-if="outline.progressPercentage < 100 && moment(outline.deadline).isBefore(Date.now())" variant="outline-danger">Details</b-button>
                                        <b-modal :id="'A'+outline.id" >
                                            <h6 class="one"> Progress Notes </h6>
                                            <div class="textarea">
                                                {{outline.progressNote}}
                                            </div>
                                            <h6 class="one"> Problems </h6>
                                            <div class="textarea">
                                                {{outline.problemNote}}
                                            </div>
                                        </b-modal> 
                                    </b-col>
                                    
                                </b-row>
                            </b-col>
                        </b-row>
                    </b-col>
                    <b-col id="task">
                        <p> Tasks to do </p>
                        <b-form-select class="bform" v-model="selectedOutline" @change="getTaskByOutlineId(selectedOutline.id)">
                            <b-form-select-option v-for="o in outline" :value="o" :key="o.id">{{o.name}}</b-form-select-option>
                        </b-form-select>    
                        <b-row class="task-item" no-gutters v-for="task in task" :key="task.id" >
                                <b-col class="task-left" id="if" cols="8"> {{task.name}} </b-col>
                                <b-col class="task-detail"> 
                                    <b-row> {{task.deadline | moment("MMMM Do YYYY")}} </b-row>
                                    <b-row>

                                    <b-button id="tt" size="sm" @click="$bvModal.show('A-' + task.id)" v-if="task.status === false && !moment(task.deadline).isBefore(Date.now())" variant="outline-dark">Details</b-button>
                                    <b-button id="tt" size="sm" @click="$bvModal.show('A-' + task.id)" v-if="task.status === true" variant="outline-success">Details</b-button>
                                    <b-button id="tt" size="sm" @click="$bvModal.show('A-' + task.id)" v-if="task.status === false && moment(task.deadline).isBefore(Date.now())" variant="outline-danger">Details</b-button>

                                        <b-modal :id="'A-' + task.id" :title="'Assigned To: ' + task.assignedTo">
                                            <h6 class="one"> Progress Notes </h6>
                                            <div class="textarea">
                                                {{task.progressNote}}
                                            </div>
                                            <h6 class="one"> Problems </h6>
                                            <div class="textarea">
                                                {{task.problemNote}}
                                            </div>
                                        </b-modal> 
                                    </b-row>
                                </b-col>
                            
                        </b-row>
                    </b-col>
                     
                        
                    
                </b-row>
            </b-col>        
        </b-col>
        <b-col> <Notification/> </b-col>
    </b-row>
    
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SideMenu from "@/components/SideMenu.vue";
import Notification from "@/components/Notification.vue";
import { IUser } from "@/types/User.d.ts";
import { IProgram } from "@/types/Program.d.ts";
import { IOutline } from "@/types/Outline.d.ts";
import { ITask } from "@/types/Task.d.ts";
import { ProgramService } from "@/services/product-service";
import { UserService } from "@/services/user-service";
import { OutlineService } from "@/services/outline-service";
import { TaskService } from "@/services/task-service";

const userService = new UserService();
const programService = new ProgramService();
const outlineService = new OutlineService();
const taskService = new TaskService();

@Component({
  name: "App",
  components: {SideMenu, Notification}
})


export default class App extends Vue {
    
    user: IUser[] = [];
    outline: IOutline[] = []
    task: ITask[] = []
    program: IProgram[] = []
    total: number = 0;
    selectedOutline: IOutline = {};
    selectedProgram: IProgram = {};

    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === null) return this.$router.push('/login');
        this.program = await programService.getProgram();
        this.user = await userService.getUser();
        this.user = this.user.filter(c => c.userAccess == "member");
        this.total = await taskService.getTotal();
    }

    async created(){
        await this.initialize();
    }

    async getOutlineByProgramId(id:number){
        this.outline = await outlineService.getOutlineByProgramId(id);
        await this.initialize();
    }

    async getTaskByOutlineId(id:number){
        this.task = await taskService.getTaskByOutlineId(id);
        await this.initialize();
    }

}
</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";

#name{
    color: blue;
}
.col-box{
    padding: 0;
}
.row-box{
    background-color: $solar-lightgrey;
    width: 100%;
    border: 1px solid lightgrey;
}

.dashboard-box{
    margin-top: 10px;
    height: 80vh;
}

#program{
    background-color: $solar-lightgrey;
    margin-left: 40px;
    margin-top: 30px;
    margin-right: 20px;
    box-shadow: 5px 10px 18px #888888;
    height: 740px;
    overflow-y: scroll;
}

.b{
    width: 90%;
}

.bform{
    width: 90%;
    margin-left: 20px;
}
#task{
    background-color: $solar-lightgrey;
    margin-top: 30px;
    margin-left: 20px;
    margin-right: 40px;
    box-shadow: 5px 10px 18px #888888;
    height: 740px;
    overflow-y: scroll;
}

.card{
    width: 80%;
    margin:0 auto;
    margin-bottom:10px ;
    font-size: 9pt;
}

.mb-2{
    width: 100%;
}

.mb-2, .my-2 {
    margin-bottom: 0rem!important;
}

#hv{
    background-color:white ;
    margin:15px;
    padding: 5px;
    padding-left:10px ;
    padding-top: 20px;
    border-radius: 15px;
    border: 1px solid lightgrey;
    font-size: 10pt;
}

#bd{
    font-size: 9pt;
    padding: 8px;
    margin-bottom: 5px;
    margin-left: 50px;
    text-align: center;
}
#vb{
    margin-left: 10px;
    margin-bottom: 3px;
}

.textarea{
    width: 90%;
    height: 250px;
    border: 1px solid lightgray;
    border-radius: 10px;
    margin: 0 auto;
    margin-bottom: 15px;
    overflow-y: scroll ;
    padding: 10px;
}

.progress{
    margin-top: 5px;
}
.one{
    margin-bottom: 15px;
    margin-left: 25px;
}

#havengers{
    background-color: $solar-lightgrey;
    margin-top: 30px;
    margin-left: 20px;
    margin-right: 20px;
    box-shadow: 5px 10px 18px #888888;
    height: 740px;
    overflow-y:scroll ;
}

#tt{
    font-size: 7.5pt;
}
.hvvr{
    margin-left: 15%;
}
#hvv{
    text-align: center;
    padding: 0;
}
#totalProgram{
    border-right: 1px solid lightgrey;
}

#totalTask{
    border-right: 1px solid lightgray;
}
h5{
    margin-top: 10px;
    text-align: center;
}

p{
    margin-top: 10px;
    font-weight: bold;
    text-align: center;
}

.task-item{
    background-color: white;
    margin: 20px;
    padding: 10px;
    border-radius: 10px;
    border: 1px solid lightgrey;
}

.task-detail{
    font-size: 8pt;
    margin-left: 25px;
}

.task-left{
    font-size: 10pt;
    
}

#notp{
    text-align: left;
}

#iff{
    font-weight: bold;
    font-size: 9pt;
    margin-bottom: 8px;
}
#if{
    font-weight: bold;
    font-size: 7.5pt;
}

.badge{
    text-align: center;
    padding: 5px;
}

#item-name{
    margin-bottom: 12px;
}

#item-nam{
    color: green;
    font-weight: bold;
    margin-bottom: 15px;
}
</style>