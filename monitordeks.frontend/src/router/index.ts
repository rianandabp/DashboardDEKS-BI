import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Dashboard from '@/views/Dashboard.vue';
import Login from '@/views/Login.vue';
import Profile from '@/views/Profile.vue';
import ChangePassword from '@/views/ChangePassword.vue';
import ManageUser from '@/views/ManageUser.vue';
import Program from '@/views/Program.vue';
import ProgramDetails from '@/views/ProgramDetails.vue';
import Partner from '@/views/Partner.vue';
import PartnerUpload from '@/views/PartnerUpload.vue';
import ManagePartner from '@/views/ManagePartner.vue';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "home",
    component: Login
  },
  {
    path: "/dashboard",
    name: "dashboard",
    component: Dashboard
  },
  {
    path: "/login",
    name: "login",
    component: Login
  },
  {
    path: "/profile",
    name: "profile",
    component: Profile
  },
  {
    path: "/changePassword",
    name: "changePassword",
    component: ChangePassword
  },
  {
    path: "/user",
    name: "manageUser",
    component: ManageUser
  },
  {
    path: "/program",
    name: "program",
    component: Program
  },
  {
    path: "/program/details",
    name: "programDetails",
    component: ProgramDetails
  },
  {
    path: "/partner",
    name: "partner",
    component: Partner
  },
  {
    path: "/partnerUpload",
    name: "partnerUpload",
    component: PartnerUpload
  },
  {
    path: "/managepartner",
    name: "managepartner",
    component: ManagePartner
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
