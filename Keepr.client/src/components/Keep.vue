<template>
  <div class="keep-img rounded shadow d-flex justify-content-between align-items-end" :style="{'background-image': 'url(' + keep.img + ')'}" data-toggle="modal" data-target="#keepModal" @click="setActive">
    <span class="name text-light">{{ keep.name }}</span>
    <img class="rounded-circle profile-icon" :src="keep.creator.picture" alt="">
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { AppState } from '../AppState'
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({

    })
    return {
      state,
      setActive() {
        AppState.ActiveKeep = props.keep
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
  background-color: rgba(0, 0, 0, 0.5);
  box-shadow: 10px -10px 10px 10px rgba(0, 0, 0, 0.5);
}
</style>
