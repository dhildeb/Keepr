import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { api } from './AxiosService'

class VaultsService {
  async getAll() {
    const res = await api.get('api/vaults')
    AppState.vaults = res.data
  }

  async getById(id) {
    try {
      const res = await api.get('api/vaults/' + id)
      AppState.ActiveVault = res.data
    } catch (error) {
      Pop.toast('vault is private')
      return true
    }
  }

  async create(data) {
    try {
      const res = await api.post('api/vaults', data)
      AppState.vaults = [res.data, ...AppState.vaults]
    } catch (error) {
      Pop.toast(error)
    }
  }

  async update(data, id) {
    const res = await api.put('api/vaults/' + id, data)
    if (res.data > 0) {
      AppState.ActiveVault.name = data.name || AppState.ActiveVault.name
      AppState.ActiveVault.description = data.description || AppState.ActiveVault.description
    }
  }

  async delete(id) {
    const res = await api.delete('api/vaults/' + id)
    AppState.vaults = AppState.vaults.filter(v => v.id !== id)
    Pop.toast(res.data, 'success')
  }
}

export const vaultsService = new VaultsService()
