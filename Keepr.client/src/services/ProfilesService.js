import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfilesService {
  async getById(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.profile = res.data
  }

  async update(id, data) {
    await api.put('api/profiles/' + id, data)
    AppState.profile.name = data.name || AppState.profile.name
    AppState.profile.picture = data.picture || AppState.profile.picture
  }

  async getKeepsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async getVaultsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }
}

export const profilesService = new ProfilesService()
