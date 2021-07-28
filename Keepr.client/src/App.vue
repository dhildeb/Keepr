<template>
  <header>
    <Navbar />
  </header>
  <main class="bg-2">
    <router-view />
  </main>
  <footer>
    <div class="text-center bg-light mt-5 p-3">
      <em>Created By Daniel Hildebrandt</em>
      <a href="https://github.com/dhildeb" title="github page" class="mdi mdi-github text-dark zoom mx-3 "></a>
      <a href="https://www.linkedin.com/in/daniel-hildebrandt/" title="linkedin page" class="mdi mdi-linkedin text-primary zoom "></a>
    </div>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
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
::-webkit-scrollbar {
  width: 1rem;
}
 ::-webkit-scrollbar-track {
   height: 40vh;
   background-color: #3ba6dc54;
 }
::-webkit-scrollbar-thumb {

  background-color: #3ba5dc;
}

</style>
