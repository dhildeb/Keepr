import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getAll() {
    const res = await api.get('api/vaults')
    AppState.vaults = res.data
  }

  async getById(id) {
    const res = await api.get('/api/vaults/' + id)
    console.log(res.data)
    AppState.ActiveVault = res.data
  }
}

export const vaultsService = new VaultsService()
