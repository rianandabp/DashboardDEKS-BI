<template>
    <div>
    <b-container fluid class="header">
        <b-row class="text-center">
            <b-col id="img"> <img src="../assets/images/logoBI.png" width="210px"></b-col>
            <b-col cols="8" id="middle" > DEKS MONITORING DASHBOARD</b-col>
            <b-col class="user" >
                <!--
                <b-row class="profile">
                    <b-col id="profileData"> <b-icon-person-fill></b-icon-person-fill> Riananda <br>
                    <b-icon-gear> </b-icon-gear> Administrator</b-col>
                    <b-col class="navbar">
                        <b-navbar type="white" >
                            <b-navbar-nav>
                                <b-nav-item-dropdown class="setting" text="Setting" right>
                                    <b-dropdown-item href="#">Change Password</b-dropdown-item>
                                    <b-dropdown-item href="#">Manage User</b-dropdown-item>
                                </b-nav-item-dropdown>
                            </b-navbar-nav>
                        </b-navbar>
                    </b-col>
                </b-row>
                -->
            </b-col>
        </b-row>
    </b-container>
    </div>
</template>


<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

@Component ({
    name: 'Header',
    components: {}
})
export default class Header extends Vue {
    
}
</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";

#img{
    padding-left:7px;
}
.header{
    background-color: $solar-grey;
    height: 50px;
}

#middle{
    padding-top: 10px ;
    font-size: 14pt;
    font-weight: bold;
}

.profile{
    padding-top:5px ;
    font-size: 9pt;
}

#profileData{
    text-align: left;
    padding-right: 0;
}

.navbar{
    padding: 0 ;
}

</style>