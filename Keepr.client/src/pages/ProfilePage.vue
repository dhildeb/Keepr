<template>
  <div class="container">
    <div class="row about text-center my-5">
      <img class="col img-fluid" :src="state.profile.picture" :alt="state.profile.name">
      <div class="col">
        <h2>
          {{ state.profile.name?.split('@')[0] }}
        </h2>
        <h4>
          {{ state.profile.email }}
        </h4>
      </div>
      <div class="col d-flex flex-column">
        <h5>Keeps: {{ state.keeps?.length }}</h5>
        <h5>Vaults: {{ state.vaults?.length }}</h5>
      </div>
    </div>
    <h1 data-toggle="modal" data-target="#createVaultModal">
      Vaults
      <i class="mdi mdi-plus text-success click add"></i>
    </h1>
    <div class="row">
      <Vaults v-for="vault in state.vaults" :key="vault.id" :vault="vault" />
    </div>
    <h1 data-toggle="modal" data-target="#createKeepModal">
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
import { computed, onMounted, reactive, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await keepsService.getAll()
      await vaultsService.getAll()
      await profilesService.getById(route.params.id)
    })
    watchEffect(() => {
      AppState.keeps = AppState.keeps.filter(k => k.creatorId === route.params.id)
      AppState.vaults = AppState.vaults.filter(k => k.creatorId === route.params.id)
    })
    const state = reactive({
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)
    })
    return {
      state
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
