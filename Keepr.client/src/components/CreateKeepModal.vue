<template>
  <div class="modal fade" id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content bg-whitish">
        <div class="modal-header justify-content-center">
          <h3>Create Keep</h3>
        </div>
        <div class="modal-body d-flex justify-content-between rounded-bottom bg-light p-5">
          <form class="form-group w-100" @submit.prevent="createKeep">
            <input id="name" class="rounded text-left input-group-text w-100" type="text" placeholder="Name">
            <input id="description" class="rounded text-left input-group-text w-100 my-3" type="text" placeholder="Description">
            <input id="tag" class="rounded text-left input-group-text w-100" type="text" placeholder="tag">
            <input id="img" class="rounded text-left input-group-text w-100 my-3" type="url" placeholder="Image Url">
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
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
export default {
  setup() {
    const router = useRouter()
    const state = reactive({
      keep: computed(() => AppState.ActiveKeep),
      account: computed(() => AppState.account)
    })
    return {
      state,
      goToProfile() {
        $('#keepModal').modal('toggle')
        router.push({ name: 'Profile', params: { id: state.keep.creator?.id } })
      },
      async deleteKeep() {
        if (await Pop.confirm('Are you sure you want to delete this?')) {
          keepsService.delete(state.keep.id)
        }
      },
      async createKeep(event) {
        $('#createKeepModal').modal('toggle')
        const form = event.target
        const data = {
          name: form.name.value,
          description: form.description.value,
          tag: form.tag.value,
          img: form.img.value
        }
        keepsService.create(data)
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
.bg-whitish{
  background-color: #e9ecef;
}
</style>
