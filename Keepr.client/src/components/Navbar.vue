<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-n d-flex justify-content-between">
    <router-link class="navbar-brand d-flex" title="Home Page" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img class="img-fluid logo" src="https://repository-images.githubusercontent.com/167710157/50aa1580-5af6-11ea-8100-0c12db10ab9f" alt="">
      </div>
    </router-link>
    <h1 class="title">
      KEEPR
    </h1>

    <span class="navbar-text p-xl">
      <button
        class="btn btn-outline-primary text-uppercase"
        @click="login"
        v-if="!user.isAuthenticated"
      >
        Login
      </button>

      <div class="dropdown click" v-else>
        <div
          class="dropdown-toggle"
          @click="state.dropOpen = !state.dropOpen"
        >
          <img
            :src="state.account.picture"
            alt="user photo"
            height="40"
            class="rounded"
          />
        </div>
        <div
          class="dropdown-menu p-0 list-group w-100 drop-pos"
          :class="{ show: state.dropOpen }"
          @click="state.dropOpen = false"
        >
          <router-link :to="{ name: 'Profile', params: {id: state.account.id} }">
            <div class="list-group-item list-group-item-action hoverable bg-n text-light p-hover">
              Profile
            </div>
          </router-link>
          <div
            class="list-group-item list-group-item-action hoverable bg-n text-light"
            @click="logout"
          >
            logout
          </div>
        </div>
      </div>
    </span>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
export default {
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      dropOpen: false
    })
    return {
      state,
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
a+div:hover{
  background-color: #4c4f56ca;
}
.p-hover:hover{
  background-color: #4c4f56ca;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}
.logo{
  max-height: 4vh;
  transform: scale(1.5);
}
.drop-pos{
  left: -89px;
  top: 48px;
}
.title{
  font-weight: 100;
  color: #a5c2ff;
}
.p-xl{
  padding-left: 25px;
}
</style>
