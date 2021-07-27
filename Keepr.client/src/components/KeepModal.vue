<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header text-center">
          <img class="img-fluid profile-icon rounded-circle click" :title="state.keep.creator?.name" :src="state.keep.creator?.picture" :alt="state.keep.creator?.name" @click="goToProfile">
          <h4 class="m-auto">
            {{ state.keep.name }}
          </h4>
          <i class="mdi mdi-delete text-danger pr-2 click" title="Delete" @click="deleteKeep" v-if="state.keep.creatorId === state.account.id && route.params?.id > 0"></i>
        </div>
        <div class="modal-body d-flex justify-content-between scrollable">
          <img class="img-fluid img" :src="state.keep.img" :alt="state.keep.name">
          <div class="w-50 d-flex flex-column justify-content-between">
            <p class="p-3">
              {{ state.keep.description }}
            </p>
            <div class="dropdown m-auto">
              <button class="btn btn-outline-primary dropdown-toggle" data-toggle="dropdown">
                Add to Vault
              </button>
              <div class="dropdown-menu">
                <p class="dropdown-item" v-for="v in state.vaults" :key="v.id" @click="addToVault(v.id)">
                  {{ v.name }}
                </p>
              </div>
            </div>
            <div class="text-center">
              <i class="mdi mdi-eye" title="total views">
                {{ state.keep.views }}
              </i>
              <i class="mdi mdi-lock px-3" title="total keeps">
                {{ state.keep.keeps }}
              </i>
              <i class="mdi mdi-share" title="total shares">
                {{ state.keep.shares }}
              </i>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import $ from 'jquery'
import { useRoute, useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'
import { vaultKeepsService } from '../services/VaultKeepsService'

export default {
  setup() {
    onMounted(() => {
      vaultsService.getAll()
    })
    const router = useRouter()
    const route = useRoute()
    const state = reactive({
      keep: computed(() => AppState.ActiveKeep),
      vaults: computed(() => AppState.vaults.filter(v => v.creatorId === AppState.account.id)),
      account: computed(() => AppState.account)
    })
    return {
      state,
      route,
      goToProfile() {
        $('#keepModal').modal('toggle')
        router.push({ name: 'Profile', params: { id: state.keep.creator?.id } })
      },
      async deleteKeep() {
        if (await Pop.confirm('Are you sure you want to delete this?')) {
          keepsService.delete(state.keep.id)
        }
      },
      async addToVault(vaultId) {
        state.keep.keeps++
        await vaultKeepsService.addToVault(vaultId, state.keep.id)
      }
    }
  }
}
</script>

<style scoped>
.img{
  object-fit: contain;
  width: 50%;
}
.mdi-delete{
  transform: scale(1.5);
}
.profile-icon{
  max-height: 5vh;
  max-width: 5vw;
}
.scrollable{
  overflow-y: auto;
}
</style>
