<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header text-center">
          <img class="img-fluid profile-icon rounded-circle click" :title="state.keep.creator?.name" :src="state.keep.creator?.picture" :alt="state.keep.creator?.name" @click="goToProfile">
          <h4 class="m-auto">
            {{ state.keep.name }}
          </h4>
          <i class="mdi mdi-delete text-danger pr-2 click" title="Delete Keep" @click="deleteKeep" v-if="state.keep.creatorId === state.account.id"></i>
        </div>
        <div class="modal-body">
          <div class="container">
            <div class="row">
              <img class="col-md-6 col-12 img-fluid img w-sm-100" :src="state.keep.img" :alt="state.keep.name">
              <div class="col d-flex flex-column justify-content-between">
                <p class="p-3">
                  {{ state.keep.description }}
                </p>
              </div>
              <div class="col-12 dropdown text-center my-4">
                <button class="btn btn-outline-primary dropdown-toggle" data-toggle="dropdown">
                  Add to Vault
                </button>
                <div class="dropdown-menu set-h">
                  <p class="dropdown-item-text click hoverable" v-for="v in state.vaults" :key="v.id" @click="addToVault(v.id)">
                    {{ v.name }}
                  </p>
                </div>
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
        if (await Pop.confirm('Are you sure you want to delete this? it cannot be undone.')) {
          $('#keepModal').modal('toggle')
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
  object-fit: cover;
  max-height: 50vh;
  width: 50%;
}
.mdi-delete{
  transform: scale(1.5);
}
.scrollable{
  overflow-y: auto;
}
.set-h{
  overflow-y: auto;
  height: 250px;
}
.hoverable:hover{
  background-color: #e5e5e5;
}
</style>
