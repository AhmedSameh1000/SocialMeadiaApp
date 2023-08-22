import { Photo } from "./photo"

export interface User {
    id: string,
    userName: string,
    name :string,
    knownAS: string,
    age: number,
    gender: string,
    created: Date,
    lastActive: Date,
    photoUrl: string,
    city: string,
    country: string
    interestes?: string,
    introduction?: string,
    lookingFor?: string,
    isLikedByCurrentUser:boolean
    photos?:Photo[]
}
