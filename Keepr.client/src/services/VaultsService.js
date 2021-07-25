import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getAll() {
    const res = await api.get('api/vaults')
    console.log(res.data)
    AppState.vaults = res.data
  }
}

export const vaultsService = new VaultsService()
