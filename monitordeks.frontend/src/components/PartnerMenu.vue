<template>
    <div class="container" >
        <div class="button-group">
            <button @click="reset(),$router.push('/login')"> Sign Out </button>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";


@Component({
  name: "App",
  components: {}
})


export default class Login extends Vue {
    async reset(){
        await sessionStorage.setItem("response", "null");
    }
}

</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";

.container{
    background-color: $solar-blue;
    width: $menu-width;
    width: 100%;
    height: 100vh;
    padding: 0;
    margin: 0;
}

.button-group{
    display: flex;
    flex-direction: column;
    
    padding: 20px;
}

button{
    padding-bottom:15px ;
    padding-top:15px ;
    color: white;
    background-color: transparent;
    border: none;
    border-bottom: 1px solid white;
    cursor: pointer;
    &:hover {
    background: lighten($solar-blue, 10%);
    transition: background-color 0.5s;
    }
    &:disabled {
        background: lighten($solar-blue, 5%);
        border-bottom: 2px solid lighten($solar-blue, 10%);
    }
    &:active {
    border:transparent;
  }

}
</style>