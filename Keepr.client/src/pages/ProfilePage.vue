<template>
  <div class="container">
    <div class="row about text-center my-5">
      <img class="img-fluid" :src="state.profile.picture" alt="profile picture">
      <div class="col">
        <div class="d-flex">
          <h1 v-if="state.editN === false">
            {{ state.profile.name?.split('@')[0] }}
          </h1>
          <h1 v-else>
            <div class="input-group">
              <input type="text" :placeholder="state.profile.name?.split('@')[0]" v-model="state.profileData.name">
              <button class="btn btn-primary input-group-append" @click="edit">
                submit
              </button>
            </div>
          </h1>
          <p v-if="state.account.id === state.route?.params?.id">
            <i class="mdi mdi-pencil click ml-3" title="Edit Profile Name" @click="state.editN = !state.editN"></i>
          </p>
        </div>
        <p class="text-left">
          {{ state.profile.email }}
        </p>
      </div>
      <div class="col d-flex flex-column">
        <b>Keeps: {{ state.keeps?.length }}</b>
        <b>Vaults: {{ state.vaults?.length }}</b>
      </div>
    </div>
    <h1 data-toggle="modal" data-target="#createVaultModal" title="Create Vault">
      Vaults
      <i class="mdi mdi-plus text-success click add"></i>
    </h1>
    <div class="row">
      <Vaults v-for="vault in state.vaults" :key="vault.id" :vault="vault" />
    </div>
    <h1 data-toggle="modal" data-target="#createKeepModal" title="Create Keep">
      Keeps
      <i class="mdi mdi-plus text-success click add"></i>
    </h1>
    <div class="masonry-with-columns p-5">
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
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    watchEffect(async() => {
      await keepsService.getAll()
      await vaultsService.getAll()
      await profilesService.getById(route.params.id)
      AppState.keeps = AppState.keeps.filter(k => k.creatorId === route.params.id)
      AppState.vaults = AppState.vaults.filter(k => k.creatorId === route.params.id)
    })
    const state = reactive({
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      editN: false,
      profileData: {},
      route: computed(() => route)
    })
    return {
      state,
      edit() {
        state.editN = false
        console.log(state.profileData)
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
