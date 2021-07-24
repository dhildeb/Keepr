<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header text-center">
          <img class="img-fluid profile-icon rounded-circle" :title="state.keep.creator.name" :src="state.keep.creator.picture" :alt="state.keep.creator.name">
          <h4>
            {{ state.keep.name }}
          </h4>
          <i class="mdi mdi-delete text-danger pr-2 click" title="Delete"></i>
        </div>
        <div class="modal-body d-flex justify-content-between">
          <img class="img-fluid img" :src="state.keep.img" :alt="state.keep.name">
          <div class="w-50 d-flex flex-column justify-content-between">
            <p class="p-3">
              {{ state.keep.description }}
            </p>
            <button class="btn btn-outline-primary m-auto">
              Add to Vault
            </button>
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
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {
  setup() {
    const state = reactive({
      keep: computed(() => AppState.ActiveKeep)
    })
    return {
      state
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
