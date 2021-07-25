<template>
  <div class="container">
    <div class="row flex-column">
      <h1>
        {{ state.vault.name }}
      </h1>
      <p>
        Keeps:
        {{ state.keeps.length }}
      </p>
    </div>
    <div class="masonry-with-columns p-5">
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
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultsService.getById(route.params.id)
      await vaultKeepsService.getKeepsByVaultId(route.params.id)
    })
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.ActiveVault)
    })
    return {
      state
    }
  }
}
</script>

<style scoped>

</style>
