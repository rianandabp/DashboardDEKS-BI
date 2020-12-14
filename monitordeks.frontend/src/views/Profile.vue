<template>
    <b-container fluid>
        <b-row class="container" no-gutters >
            <b-col class="sidemenu"> <SideMenu/></b-col>
            <b-col cols="9">
                <div class="container">
                    <div class="form">
                        <h3>Profile</h3>

                        <b-row >
                            <b-col class="lf">Name: </b-col>
                            <b-col class="rh">{{user.name}} </b-col>
                        </b-row>

                        <b-row >
                            <b-col class="lf">Username:  </b-col>
                            <b-col class="rh">{{user.usernameId}} </b-col>
                        </b-row>

                        <b-row  >
                            <b-col class="lf">User Access:  </b-col>
                            <b-col class="rh">{{user.userAccess}} </b-col>
                        </b-row>

                        <b-col>
                            <b-button class="bb" variant="dark" size="md" @click="$router.push('/changePassword')">Change Password</b-button>
                            <b-button class="bb" variant="dark" size="md" :disabled.sync="this.state" @click="$router.push('/user')">Manage User</b-button>
                        </b-col>
                        

                    </div>
                </div>
            </b-col>
            <b-col>
            </b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import SideMenu from "@/components/SideMenu.vue";
import Notification from "@/components/Notification.vue";
import {IUser} from "@/types/User.d.ts";
import { UserService } from "@/services/user-service";

const userService = new UserService();
@Component({
  name: "App",
  components: {SideMenu, Notification}
})
export default class App extends Vue {
    user: IUser = {};
    state: boolean = true;

    checkState(){
        if(this.user.userAccess == "administrator") this.state = false;
        else this.state = true;
    }
    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === null) return this.$router.push('/login');
        this.user = await userService.getUserByUsername(sessionStorage.getItem("username")!);
        await this.checkState();
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

.container, .container-fluid, .container-lg, .container-md, .container-sm, .container-xl {
    width: 100%;
    padding-right: 0px;
    padding-left: 0px;
    margin-right: auto;
    margin-left: auto;
}

.form{
    text-align: center;
    padding: 40px;
    margin: 0 auto;
    margin-top: 5%;
    width: 50%;
    background-color:$solar-lightgrey ;
    border-radius:20px ;
    box-shadow: 5px 10px 18px #888888;
}

h3{
    padding-bottom: 30px ;
}

.lf{
    text-align: right;
    font-weight: bold;
}

.rh{
    text-align: left;
    padding-bottom:15px;
}

.bb{
    margin: 15px;
}
</style>