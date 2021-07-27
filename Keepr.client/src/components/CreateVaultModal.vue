<template>
  <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content bg-3 text-light border border-dark">
        <div class="modal-header justify-content-center">
          <h3>Create Vault</h3>
        </div>
        <div class="modal-body bg-light d-flex justify-content-between rounded-bottom p-5">
          <form class="w-100" @submit.prevent="createVault">
            <input id="name" class="rounded text-left input-group-text w-100 mb-3" type="text" placeholder="Name">
            <input id="description" class="rounded text-left input-group-text w-100 mb-3" type="text" placeholder="Description">
            <p class="text-dark">
              Private
              <input id="isPrivate" type="checkbox" v-model="checked">
            </p>
            <button class="btn btn-success">
              Submit
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import $ from 'jquery'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      isPrivate: false
    })
    return {
      state,
      async createVault(event) {
        $('#createVaultModal').modal('toggle')
        const form = event.target
        const data = {
          name: form.name.value,
          description: form.description.value,
          isPrivate: form.isPrivate.checked
        }
        await vaultsService.create(data)
        form.reset()
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
</style>
