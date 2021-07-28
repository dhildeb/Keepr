<template>
  <div class="container">
    <div class="row about text-center my-5 p-3">
      <!-- profile name -->
      <div class="col-12 d-flex justify-content-center mb-3">
        <h1 v-if="state.editN === false">
          {{ state.profile.name?.split('@')[0] }}
        </h1>
        <div class="input-group py-3" v-else>
          <input type="text" :placeholder="state.profile.name?.split('@')[0]" v-model="state.profileData.name">
          <button class="btn btn-primary input-group-append" @click="edit">
            submit
          </button>
        </div>
        <p class="m-0" v-if="state.account.id === state.route.params.id">
          <i class="mdi mdi-pencil click" title="Edit Profile Name" @click="state.editN = !state.editN"></i>
        </p>
      </div>
      <!-- profile picture -->
      <div class="col-6 d-flex justify-content-start">
        <img class="img-fluid" :src="state.profile.picture" alt="profile picture" v-if="state.editI === false">
        <div class="input-group" v-else>
          <input type="text" :placeholder="state.profile?.picture" v-model="state.profileData.picture">
          <button class="btn btn-primary input-group-append" @click="edit">
            submit
          </button>
        </div>
        <p v-if="state.account.id === state.route.params.id">
          <i class="mdi mdi-pencil click ml-3" title="Edit Profile Picture" @click="state.editI = !state.editI"></i>
        </p>
      </div>
      <!-- stats -->
      <div class="col-6 d-flex flex-column align-items-center mt-4">
        <b>Keeps: {{ state.keeps?.length }}</b>
        <b>Vaults: {{ state.vaults?.length }}</b>
      </div>
      <!-- profile email -->
      <p class="text-left mt-3">
        {{ state.profile.email }}
      </p>
    </div>
    <h1>
      Vaults
      <i class="mdi mdi-plus text-success click add" data-toggle="modal" data-target="#createVaultModal" title="Create Vault" v-if="state.account.id === state.route.params.id"></i>
    </h1>
    <div class="row">
      <Vaults v-for="vault in state.vaults" :key="vault.id" :vault="vault" />
    </div>
    <h1>
      Keeps
      <i class="mdi mdi-plus text-success click add" data-toggle="modal" data-target="#createKeepModal" title="Create Keep" v-if="state.account.id === state.route.params.id"></i>
    </h1>
    <div class="masonry-with-columns mt-5">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
  <KeepModal />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed, reactive, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    watchEffect(async() => {
      await profilesService.getById(route.params.id)
      await profilesService.getKeepsByProfileId(route.params.id)
      await profilesService.getVaultsByProfileId(route.params.id)
    })
    const state = reactive({
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      editN: false,
      editI: false,
      profileData: {},
      route: computed(() => route)
    })
    return {
      state,
      edit() {
        state.editN = false
        state.editI = false
        profilesService.update(state.account.id, state.profileData)
        state.profileData = {}
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
.about{
  contain: content;
}
.add{
  transition: all .1s linear;
}
.add:hover{
  text-shadow: 1px 3px 4px black;
}
</style>
