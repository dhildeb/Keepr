<template>
  <div class="masonry-with-columns p-5">
    <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
  </div>
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
body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    background-color: rgba(0, 0, 0, 0.75);
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
  }
  @for $i from 1 through 5000 {
    div:nth-child(#{$i}) {
      $h: (random(400) + 100) + px;
      height: $h;
      // line-height: $h;
    }
  }
}
</style>
