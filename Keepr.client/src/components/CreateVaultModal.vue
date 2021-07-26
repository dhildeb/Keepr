<template>
  <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content bg-primary">
        <div class="modal-header">
          <h3>Create Vault</h3>
        </div>
        <div class="modal-body d-flex justify-content-between">
          <form @submit.prevent="createVault">
            <input id="name" class="rounded" type="text" placeholder="Name">
            <input id="description" class="rounded" type="text" placeholder="Description">
            <p>
              Private
            </p>
            <input id="isPrivate" type="checkbox">
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
import Pop from '../utils/Notifier'
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
          isPrivate: form.isPrivate.value
        }
        console.log(data)
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
