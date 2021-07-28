import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfilesService {
  async getById(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.profile = res.data
  }

  async update(id, data) {
    const res = await api.put('api/profiles/' + id, data)
    console.log(res.data)
  }

  async getKeepsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    console.log(res.data)
    AppState.keeps = res.data
  }

  async getVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    console.log(res.data)
    AppState.vaults = res.data
  }
}

export const profilesService = new ProfilesService()
