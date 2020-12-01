<template>
    <div id="app">
        <div class="app-menu">
        <Header/>
        </div>
        <div class="app-content">
        <router-view/>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Header from "@/components/Header.vue";
import SideMenu from "@/components/SideMenu.vue";
import Dashboard from "@/views/Dashboard.vue";
import {INotification} from "@/types/Notification.d.ts";

@Component({
  name: "App",
  components: {Header,SideMenu, Dashboard}
})
export default class App extends Vue {
    
}
</script>

<style lang="scss">
@import "@/scss/global.scss";

.container{
    max-width: 100%;
    padding: 0;
}


</style>
