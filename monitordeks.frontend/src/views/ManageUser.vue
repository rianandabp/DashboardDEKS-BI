<template>
    <b-row class="container" no-gutters>
        <b-col> <SideMenu/> </b-col>
        <b-col cols="9">
            <div class="box">
                <b-row> <b-col id="us"> 
                    <h4>User List</h4> </b-col> <b-col id="au"><b-button size="sm" variant="primary" @click="$bvModal.show('addUser')"> Add User </b-button> </b-col>
                </b-row>
                <b-modal :id="'addUser'" @ok="addUser(tempUser)" @hidden="resetModal()" :title="'Add User'">
                    <b-form-group label="Name" label-for="text" @submit.stop.prevent>
                    <b-form-input
                        size="sm"
                        placeholder="Name"
                        v-model="tempUser.name"
                    ></b-form-input>
                    </b-form-group>

                    <b-form-group label="Username" label-for="text" @submit.stop.prevent>
                    <b-form-input
                        size="sm"
                        placeholder="Username"
                        v-model="tempUser.usernameId"
                    ></b-form-input>
                    </b-form-group>

                    <b-form-group label="Password" >
                    <b-form-input
                        type="password"
                        size="sm"
                        placeholder="Password"
                        v-model="tempUser.password"
                    ></b-form-input>
                    </b-form-group>

                    <b-form-group label="Re-type password" >
                    <b-form-input
                        type="password"
                        size="sm"
                        placeholder="Re-type password"
                        v-model="pass"
                        :state="state()"
                        aria-describedby="input-live-password"
                    ></b-form-input>
                    <b-form-invalid-feedback id="input-live-password">
                        Password does not match!
                    </b-form-invalid-feedback>
                    </b-form-group>

                    <b-form-group label="Access">
                        <b-form-radio  name="some-radios" value="administrator" v-model="tempUser.userAccess">Administrator</b-form-radio>
                        <b-form-radio  name="some-radios" value="member" v-model="tempUser.userAccess">Member</b-form-radio>
                    </b-form-group>
                </b-modal>
                <div class="table"> <table id="tb" >
                        <tr>
                            <th> Id </th>
                            <th> Name </th>   
                            <th> Username </th>
                            <th> User Access </th>
                            <th> Edit </th>
                            <th> Delete </th>
                        </tr>
                        <tr v-for="(item,index) in user" :key="item.usernameId">

                            <td> {{index + 1}} </td>
                            <td> {{item.name}} </td>
                            <td> {{item.usernameId}} </td>
                            <td> {{item.userAccess}} </td>
                            <td> <b-icon icon="clipboard" @click="$bvModal.show(item.usernameId)"></b-icon> </td>
                            <td> <b-icon id="dlt" icon="x-circle" @click="$bvModal.show('confirmationDelete-' + index + 1)"></b-icon> </td>
                            <b-modal :id="'confirmationDelete-' + index + 1" @ok="deleteUser(item.usernameId)">
                                {{item.name}} access will be deleted
                            </b-modal>
                            <b-modal :id="item.usernameId" :title="'Edit User'" @hidden="resetModal" @show="loadUser(item.usernameId)" @ok="updateUser(item.usernameId)">
                                <b-form-group label="Name" label-for="text" @submit.stop.prevent>
                                    <b-form-input
                                        size="sm"
                                        placeholder="Name"
                                        v-model="tempUser.name"
                                    ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Password" >
                                    <b-form-input
                                        type="password"
                                        size="sm"
                                        placeholder="Password"
                                        v-model="tempUser.password"
                                    ></b-form-input>
                                    </b-form-group>

                                    <b-form-group label="Re-type password" >
                                    <b-form-input
                                        
                                        type="password"
                                        size="sm"
                                        placeholder="Re-type password"
                                        v-model="pass"
                                        :state="state()"
                                        aria-describedby="input-live-password"
                                    ></b-form-input>
                                    <b-form-invalid-feedback id="input-live-password">
                                        Password does not match!
                                    </b-form-invalid-feedback>
                                    </b-form-group>

                                    <b-form-group label="Access">
                                        <b-form-radio  name="some-radios" value="administrator" v-model="tempUser.userAccess">Administrator</b-form-radio>
                                        <b-form-radio  name="some-radios" value="member" v-model="tempUser.userAccess">Member</b-form-radio>
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
import { IUser } from "@/types/User.d.ts";
import { UserService } from "@/services/user-service";
import Vuex from 'vuex'

Vue.use(Vuex)
const userService = new UserService();

@Component({
  name: "App",
  components: {SideMenu, Notification}
})

export default class App extends Vue {

    user: IUser[] = []
    tempUser: IUser = {
        name: '',
        punctualityRating: 0
    };
    pass: string = ''

    state(){
        if(this.pass === '') return null
        if(this.tempUser.password === this.pass) return true
        return false
    }

    resetModal(){
        this.tempUser.name = ''
        this.tempUser.usernameId = ''
        this.tempUser.password = ''
        this.pass = ''
    }

    loadUser(id:string){
        const p = this.user.find(c => c.usernameId==id)
        this.tempUser.name = p?.name
    }
    async updateUser(username:string) {
        if(!this.state()) return
        this.tempUser.usernameId = username;
        console.log(this.tempUser)
        await userService.updateUser(this.tempUser)
        await this.initialize()
    }

    async deleteUser(username:string){
        await userService.deleteUser(username);
        await this.initialize();
    }

    async addUser(newUser: IUser) {
        if(!this.state()) return;   
        await userService.addUser(newUser);
        await this.initialize();
    }

    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === "null") return this.$router.push('/login');
        if(sessionStorage.getItem("access") != "administrator") return this.$router.back();
        this.user = await userService.getUser();
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

h5{
    text-align: center;
    margin-top: 10px;
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
    text-align: right;
}
#tb{
    width: 100%;
}
.form{
    text-align: center;
}
</style>