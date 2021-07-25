<template>
  <div class="masonry-with-columns p-5">
    <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
  </div>
  <KeepModal />
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  setup() {
    onMounted(() => {
      keepsService.getAll()
    })
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">

</style>
