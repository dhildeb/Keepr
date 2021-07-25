import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultKeepsService {
  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    console.log(res.data)
    AppState.keeps = res.data
  }
}
export const vaultKeepsService = new VaultKeepsService()
