<template>
    <div class="container">
        <form class="form">
            <img src="../assets/images/logoBI.png" width="320px">
            <h3>Sign In</h3>

            <div class="form-group">
                <label>Username</label>
                <b-form-input v-model="temp.usernameId" :state="nameState()" aria-describedby="input-live-username"> </b-form-input>
                <b-form-invalid-feedback id="input-live-username">
                    Check your username
                </b-form-invalid-feedback>
            </div>

            <div class="form-group">
                <label>Password</label>
                <b-form-input v-model="temp.password" :state="nameState()"  type="password" aria-describedby="input-live-password"> </b-form-input>
                <b-form-invalid-feedback id="input-live-password">
                    Check your password
                </b-form-invalid-feedback>
            </div>

            <button type="button" class="btn btn-dark btn-lg btn-block"  @click="auth" @submit.stop.prevent>Sign In</button>
            <button type="button" class="btn btn-dark btn-lg btn-block"  @click="authPartner">Login for Partner</button>
            
            <p>
                For login issues, please contact your administrator.
            </p>

        </form>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import {IUser} from "@/types/User.d.ts";
import { UserService } from "@/services/user-service";
import { PartnerService } from "@/services/partner-service";

const partnerService = new PartnerService();
const userService = new UserService();

@Component({
  name: "App",
  components: {}
})


export default class Login extends Vue {
    
    

    temp: IUser = {
        name: "",
        userAccess: "",
        punctualityRating: 0
    }

    nameState(){
        if(sessionStorage.getItem("response") === "false") return false;
        return null;
    }

    currentUser: IUser = {}

    async auth(){
        const response =  await userService.auth((this.temp.usernameId+'|'+this.temp.password).toString());
        
        await sessionStorage.setItem("response",response);
        await sessionStorage.setItem("username",this.temp.usernameId!);
        if(sessionStorage.getItem("response") == "true")
        {
            this.temp = await userService.getUserByUsername(sessionStorage.getItem("username")!);
            await sessionStorage.setItem("access", this.temp.userAccess!);
        } 
            
        this.$router.push('/dashboard');
    }

    async authPartner(){
        const response =  await partnerService.auth((this.temp.usernameId+'|'+this.temp.password).toString());
        
        await sessionStorage.setItem("response",response);
        await sessionStorage.setItem("username",this.temp.usernameId!);
            
        this.$router.push('/partnerUpload');
    }

    async initialize(){
        console.log(sessionStorage.getItem("response"));
    }

    async created(){
        await this.initialize();
    }
    
    

}

</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";

    .container{
        background-color: $solar-blue;
        max-width: 100%;
        height: 100vh;
        padding-top: 10% ;
        font-size: 11pt;
    }
    .form{
        text-align: center;
        padding: 40px;
        margin: 0 auto;
        width: 400px;
        background-color:$solar-lightgrey ;
        border-radius:20px ;
    }

    h3{
        padding-bottom: 30px ;
    }
    .form-group{
        padding-left: 30px ;
        padding-right: 30px ;
    }

    button{
        margin-top: 40px;
        margin-bottom: 10px;
    }
</style>