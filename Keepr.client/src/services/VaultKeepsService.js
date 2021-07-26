import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { api } from './AxiosService'

class VaultKeepsService {
  async getKeepsByVaultId(id) {
    try {
      const res = await api.get(`api/vaults/${id}/keeps`)
      console.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      Pop.toast(error)
    }
  }
}
export const vaultKeepsService = new VaultKeepsService()
