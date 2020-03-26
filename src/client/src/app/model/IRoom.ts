export interface IRoom {
	id: string;
	name: string;
	enterMessage: string;
	description: string;
	position: {
		x: number;
		y: number;
	};
	westId?: string;
	northId?: string;
	southId?: string;
	eastId?: string;
}
