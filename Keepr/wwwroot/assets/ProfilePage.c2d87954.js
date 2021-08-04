import{A as t,r as e,c as a,p as l,e as s,j as i,d as o,o as p,a as d,b as c,t as r,h as n,B as u,k as m,F as f,m as y,f as k,i as v}from"./vendor.822fba46.js";import{a as g,A as b}from"./index.039bc042.js";const x=new class{async getById(t){const e=await g.get("api/profiles/"+t);b.profile=e.data}async update(t,e){await g.put("api/profiles/"+t,e),b.profile.name=e.name||b.profile.name,b.profile.picture=e.picture||b.profile.picture}async getKeepsByProfileId(t){const e=await g.get(`api/profiles/${t}/keeps`);b.keeps=e.data}async getVaultsByProfileId(t){const e=await g.get(`api/profiles/${t}/vaults`);b.vaults=e.data}};const I={name:"Profile",setup(){const l=i();t((async()=>{await x.getById(l.params.id),await x.getKeepsByProfileId(l.params.id),await x.getVaultsByProfileId(l.params.id)}));const s=e({profile:a((()=>b.profile)),account:a((()=>b.account)),keeps:a((()=>b.keeps)),vaults:a((()=>b.vaults)),editN:!1,editI:!1,profileData:{},route:a((()=>l))});return{state:s,edit(){s.editN=!1,s.editI=!1,x.update(s.account.id,s.profileData),s.profileData={}}}}},h=k();l("data-v-0db6132c");const w={class:"container"},V={class:"row about text-center my-5 p-3"},K={class:"col-12 d-flex justify-content-center mb-3"},C={key:0},P={key:1,class:"input-group py-3"},B={key:2,class:"m-0"},D={class:"col-6 d-flex justify-content-start"},N={key:1,class:"input-group"},j={key:2},M={class:"col-6 d-flex flex-column align-items-center mt-4"},A={class:"text-left mt-3"},E=v(" Vaults "),U={key:0,class:"mdi mdi-plus text-success click add","data-toggle":"modal","data-target":"#createVaultModal",title:"Create Vault"},$={class:"row"},_=v(" Keeps "),F={key:0,class:"mdi mdi-plus text-success click add","data-toggle":"modal","data-target":"#createKeepModal",title:"Create Keep"},q={class:"masonry-with-columns mt-5"};s();const z=h(((t,e,a,l,s,i)=>{var k,v,g,b,x;const I=o("Vaults"),h=o("Keep"),z=o("KeepModal"),G=o("CreateKeepModal"),H=o("CreateVaultModal");return p(),d(f,null,[c("div",w,[c("div",V,[c("div",K,[!1===l.state.editN?(p(),d("h1",C,r(null==(k=l.state.profile.name)?void 0:k.split("@")[0]),1)):(p(),d("div",P,[n(c("input",{type:"text",placeholder:null==(v=l.state.profile.name)?void 0:v.split("@")[0],"onUpdate:modelValue":e[1]||(e[1]=t=>l.state.profileData.name=t)},null,8,["placeholder"]),[[u,l.state.profileData.name]]),c("button",{class:"btn btn-primary input-group-append",onClick:e[2]||(e[2]=(...t)=>l.edit&&l.edit(...t))}," submit ")])),l.state.account.id===l.state.route.params.id?(p(),d("p",B,[c("i",{class:"mdi mdi-pencil click",title:"Edit Profile Name",onClick:e[3]||(e[3]=t=>l.state.editN=!l.state.editN)})])):m("",!0)]),c("div",D,[!1===l.state.editI?(p(),d("img",{key:0,class:"img-fluid",src:l.state.profile.picture,alt:"profile picture"},null,8,["src"])):(p(),d("div",N,[n(c("input",{type:"text",placeholder:null==(g=l.state.profile)?void 0:g.picture,"onUpdate:modelValue":e[4]||(e[4]=t=>l.state.profileData.picture=t)},null,8,["placeholder"]),[[u,l.state.profileData.picture]]),c("button",{class:"btn btn-primary input-group-append",onClick:e[5]||(e[5]=(...t)=>l.edit&&l.edit(...t))}," submit ")])),l.state.account.id===l.state.route.params.id?(p(),d("p",j,[c("i",{class:"mdi mdi-pencil click ml-3",title:"Edit Profile Picture",onClick:e[6]||(e[6]=t=>l.state.editI=!l.state.editI)})])):m("",!0)]),c("div",M,[c("b",null,"Keeps: "+r(null==(b=l.state.keeps)?void 0:b.length),1),c("b",null,"Vaults: "+r(null==(x=l.state.vaults)?void 0:x.length),1)]),c("p",A,r(l.state.profile.email),1)]),c("h1",null,[E,l.state.account.id===l.state.route.params.id?(p(),d("i",U)):m("",!0)]),c("div",$,[(p(!0),d(f,null,y(l.state.vaults,(t=>(p(),d(I,{key:t.id,vault:t},null,8,["vault"])))),128))]),c("h1",null,[_,l.state.account.id===l.state.route.params.id?(p(),d("i",F)):m("",!0)]),c("div",q,[(p(!0),d(f,null,y(l.state.keeps,(t=>(p(),d(h,{key:t.id,keep:t},null,8,["keep"])))),128))])]),c(z),c(G),c(H)],64)}));I.render=z,I.__scopeId="data-v-0db6132c";export default I;