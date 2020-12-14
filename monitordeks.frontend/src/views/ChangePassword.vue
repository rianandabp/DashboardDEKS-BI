<template>
    <b-container fluid>
        <b-row class="container" no-gutters >
            <b-col class="sidemenu"> <SideMenu/></b-col>
            <b-col cols="9">
                <div class="container">
                    <form class="form">
                        <h3>Change Password</h3>

                        <b-row class="form-group">
                            <b-col>New Password</b-col>
                            <b-col><b-form-input v-model="pass"  type="password"> </b-form-input></b-col>
                        </b-row>

                        <b-row class="form-group">
                            <b-col>Re-type New Password</b-col>
                            <b-col>
                                <b-form-input v-model="tempUser.password" :state="state()"  type="password" aria-describedby="input-live-password"> </b-form-input>
                                <b-form-invalid-feedback id="input-live-password">
                                    Password does not match!
                                </b-form-invalid-feedback>
                            </b-col>
                        </b-row>

                        <b-button id="bb" variant="dark" size="md"  @click="$bvModal.show('ch'), updateUser()">Submit</b-button>
                        <b-modal id="ch">
                            <p v-if="state()">Success!</p>
                            <p v-if="!state()">Password does not match!</p>
                        </b-modal>

                    </form>
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
import { IUser } from "@/types/User.d.ts";
import { UserService } from "@/services/user-service";

const userService = new UserService();

@Component({
  name: "App",
  components: {SideMenu, Notification}
})

export default class App extends Vue {
    tempUser: IUser = {
        name: '',
        punctualityRating: 0
    };
    pass: string = '';

    state(){
        if(this.tempUser.password === '') return null;
        if(this.pass === this.tempUser.password) return true;
        return false; 
    }

    async updateUser() {
        if(this.state()) await userService.updateUser(this.tempUser);
    }

    async initialize(){
        if(sessionStorage.getItem("response") === "false" || sessionStorage.getItem("response") === null) return this.$router.push('/login');
        this.tempUser = await userService.getUserByUsername(sessionStorage.getItem("username")!);
        this.tempUser.password = '';
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
.form-group{
    padding-left: 30px ;
    padding-right: 30px ;
}

#bb{
    margin-top: 20px;
}
</style>