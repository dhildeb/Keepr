<template>
  <div class="col-lg-3 col-md-4 col-sm-6 col-12 vault mb-4" v-if="!vault.private">
    <div class="border border-dark shadow click rounded h-100 w-100 d-flex align-items-center justify-content-center text-center">
      <i class="mdi mdi-delete text-danger position-absolute delete" @click="deleteVault(vault.id)"></i>
      <router-link class="route" :to="{name: 'Vault', params: {id: vault.id}}">
        <b>
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
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    const state = reactive({

    })
    return {
      state,
      async deleteVault(id) {
        if (await Pop.confirm('Do you really want to delete this vault?')) {
          vaultsService.delete(id)
        }
      }
    }
  }
}
</script>

<style scoped>
.vault{
  object-fit: contain;
  background-image: url('https://repository-images.githubusercontent.com/167710157/50aa1580-5af6-11ea-8100-0c12db10ab9f');
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center;
  height: 20vh;
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

</style>
