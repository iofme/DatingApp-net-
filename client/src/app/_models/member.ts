import { Data } from "@angular/router"
import { Photo } from "./photo"
2
export interface Member {
    id: number
    userName: string
    age: number
    photoUrl: string
    knownAs: string
    created: Date
    lastActive: Date
    gender: string
    introduction: string
    interests: string
    lookingFor: string
    city: string
    country: string
    photos: Photo[]
  }
  
  