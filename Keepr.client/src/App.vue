<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>
    <div class="text-center bg-light mt-5 p-3">
      <em>Created By Daniel Hildebrandt</em>
      <a href="https://github.com/dhildeb" class="mdi mdi-github text-dark zoom mx-3 "></a>
      <a href="https://www.linkedin.com/in/daniel-hildebrandt/" class="mdi mdi-linkedin text-primary zoom "></a>
    </div>
  </footer>
</template>

<script>
import { computed, watchEffect } from 'vue'
import { AppState } from './AppState'
import { accountService } from './services/AccountService'
export default {
  name: 'App',
  setup() {
    watchEffect(() => {
      accountService.getAccount()
    })
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
.zoom{
  transform: scale(1.5);
}
</style>
