<template>
  <div class="col-lg-3 col-md-4 col-sm-6 col-12 box mb-4" v-if="route.params.id == state.account.id || !vault.isPrivate">
    <small class="position-absolute private" v-if="vault.isPrivate">private</small>
    <div class="border border-dark shadow click rounded vault h-100 w-100 d-flex align-items-center justify-content-center text-center">
      <i class="mdi mdi-delete text-danger position-absolute delete" title="Delete Vault" @click="deleteVault(vault.id)" v-if="route.params.id === state.account.id"></i>
      <router-link class="route" :to="{name: 'Vault', params: {id: vault.id}}">
        <b class="p-3">
          {{ vault.name }}
        </b>
      </router-link>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    const route = useRoute()
    const state = reactive({
      account: computed(() => AppState.account)
    })
    return {
      state,
      async deleteVault(id) {
        if (await Pop.confirm('Do you really want to delete this vault?')) {
          vaultsService.delete(id)
        }
      },
      route
    }
  }
}
</script>

<style scoped>
.box{

  height: 20vh;
}
.vault{
  object-fit: contain;
  background-image: url('https://repository-images.githubusercontent.com/167710157/50aa1580-5af6-11ea-8100-0c12db10ab9f');
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center;
  background-color: #f2f0ef;
  opacity: .6;
  transition: all .2s linear;
}
.vault:hover{
  opacity: 1;
}
.route{
  text-decoration: none;
  color: black;
}
.delete{
  top: 0;
  right: 20px;
}
.private{
  left: 25px;
}
</style>
