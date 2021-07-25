<template>
  <div class="about text-center">
    <img class="img-fluid" :src="state.profile.picture" :alt="state.profile.name">
    <h4>{{ state.profile.name }}</h4>
    <h2>{{ state.profile.email }}</h2>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    onMounted(() => {
      profilesService.getById(route.params.id)
    })
    const state = reactive({
      profile: computed(() => AppState.profile)

    })
    return {
      state
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
