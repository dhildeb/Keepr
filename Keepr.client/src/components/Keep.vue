<template>
  <div class="keep-img rounded shadow d-flex justify-content-between align-items-end px-2" :style="{'background-image': 'url(' + keep.img + ')'}" data-toggle="modal" data-target="#keepModal" @click="setActive">
    <i class="mdi mdi-delete text-danger align-self-start" title="Remove from Vault" @click.stop="removeKeep" v-if="state.vault.creatorId === state.account.id && route.params?.id > 0"></i>
    <div class="d-flex justify-content-between align-items-end">
      <span class="name text-light">{{ keep.name }}</span>
      <img class="rounded-circle profile-icon" :src="keep.creator.picture" alt="profile img" :title="'Go to'+keep.creator.name?.split('@')[0]+' profile'" @click.stop="goThere">
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { vaultKeepsService } from '../services/VaultKeepsService'
import Pop from '../utils/Notifier'
import { useRoute, useRouter } from 'vue-router'
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const router = useRouter()
    const route = useRoute()
    const state = reactive({
      vault: computed(() => AppState.ActiveVault),
      account: computed(() => AppState.account)
    })
    return {
      state,
      route,
      setActive() {
        keepsService.setActive(props.keep)
      },
      async removeKeep() {
        if (await Pop.confirm('Do you really want to remove this from your vault?')) {
          await vaultKeepsService.delete(props.keep.vaultKeepId)
        }
      },
      goThere() {
        router.push({ name: 'Profile', params: { id: props.keep.creator.id } })
      }
    }
  }
}
</script>

<style scoped>
.keep-img{
  object-fit: contain;
  background-size: cover;
  background-repeat: no-repeat;
  cursor: pointer;
  transition: all .25s linear;
  contain: size;
}
.keep-img:hover{
  transform: scale(1.025);
}
.profile-icon{
  max-height: 5vh;
  max-width: 5vw;
}
.name{
    text-shadow: 0px 1px 4px black;
}

</style>
