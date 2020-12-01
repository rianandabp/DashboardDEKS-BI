<template>
    <div class="notification">
        
        <p> Notification </p>
        <b-col class="notification-data" v-for="item in notification" :key="item.id" >
            <b-row no-gutters> 
                <b-col class="data">
                    <b-row >  {{item.description}} </b-row>
                    <b-row id="date" >  {{item.createdOn | moment("MMMM Do YYYY, h:mm:ss a")}} </b-row>
                </b-col>
            </b-row>
        </b-col>
            
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import {INotification} from "@/types/Notification.d.ts";
import { NotificationService } from "@/services/notification-service";

const notificationService = new NotificationService();

@Component({
  name: "Notification",
  components: {}
})
export default class App extends Vue {
    notification: INotification[] = [];

    async initialize(){
        this.notification = await notificationService.getNotification();
    }

    async created(){
        await this.initialize();
    }
    
}
</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";
.notification{
    padding: 0;
    background-color: $solar-lightgrey;
    height: 100vh;
    overflow-y: scroll;

}

p{
    font-weight: bold;
    padding-top: 10px ;
    text-align: center;
}

.notification-data{
    text-align: left;
    font-size:7.5pt ;
    background-color: white;
    border-radius: 10px;
    border: 1px solid lightgrey;
    margin: 0 auto;
    margin-bottom: 10px;
    width: 90%;
}

.data{
    padding:10px ;
}

#date{
    font-weight: bold;
}
</style>