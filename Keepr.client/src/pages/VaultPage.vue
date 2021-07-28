<template>
  <div class="container">
    <div class="row flex-column p-3">
      <div class="d-flex justify-content-center">
        <small v-if="state.vault.isPrivate">private</small>
        <h1 v-if="!state.editN">
          {{ state.vault.name }}
        </h1>
        <h1 v-else>
          <div class="input-group">
            <input type="text" :placeholder="state.vault.name" v-model="state.vaultData.name">
            <button class="btn btn-primary input-group-append" @click="edit">
              submit
            </button>
          </div>
        </h1>
        <p v-if="state.account.id === state.vault.creatorId">
          <i class="mdi mdi-pencil click ml-3" title="Edit Vault" @click="state.editN = !state.editN"></i>
        </p>
      </div>
      <div class="d-flex">
        <h2 v-if="state.editD === false">
          {{ state.vault.description }}
        </h2>
        <h2 v-else>
          <div class="input-group" @submit.prevent="editD">
            <input type="text" :placeholder="state.vault.description" v-model="state.vaultData.description">
            <button class="btn btn-primary input-group-append" type="submit" @click="edit">
              submit
            </button>
          </div>
        </h2>
        <p v-if="state.account.id === state.vault.creatorId">
          <i class="mdi mdi-pencil click ml-3" title="Edit Vault" @click="state.editD = !state.editD"></i>
        </p>
      </div>
      <p>
        Keeps:
        {{ state.keeps.length }}
      </p>
    </div>
    <div class="masonry-with-columns mt-5">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
  <KeepModal />
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed, onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.ActiveVault),
      account: computed(() => AppState.account),
      editD: false,
      editN: false,
      vaultData: {}
    })
    onMounted(async() => {
      const router = useRouter()
      if (await vaultsService.getById(route.params.id)) {
        router.push({ name: 'Home' })
      }
      await vaultKeepsService.getKeepsByVaultId(route.params.id)
    })
    return {
      state,
      edit() {
        state.editN = false
        state.editD = false
        vaultsService.update(state.vaultData, route.params.id)
        state.vaultData = {}
      }
    }
  }
}
</script>

<style scoped>

</style>
