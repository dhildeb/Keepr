import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { api } from './AxiosService'

class VaultsService {
  async getAll() {
    const res = await api.get('api/vaults')
    AppState.vaults = res.data
  }

  async getById(id) {
    const res = await api.get('api/vaults/' + id)
    console.log(res.data)
    AppState.ActiveVault = res.data
  }

  async create(data) {
    try {
      const res = await api.get('api/vaults', data)
      console.log(res.data)
      AppState.vaults = [res.data, ...AppState.vaults]
    } catch (error) {
      Pop.toast(error)
    }
  }
}

export const vaultsService = new VaultsService()
