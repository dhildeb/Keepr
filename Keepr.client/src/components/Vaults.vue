<template>
  <div class="col-3 border border-dark shadow click rounded vault" v-if="!vault.private">
    <p>
      {{ vault.name }}
    </p>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    const state = reactive({

    })
    return {
      state
    }
  }
}
</script>

<style scoped>
.vault{

}

</style>
